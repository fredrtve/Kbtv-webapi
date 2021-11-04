using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using BjBygg.Core.Entities;
using BjBygg.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class OptimisticCommandMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserCommandStatusRepository _statusRepository;

        public OptimisticCommandMiddleware(RequestDelegate next, UserCommandStatusRepository statusRepository)
        {
            _next = next;
            _statusRepository = statusRepository;
        }

        public async Task Invoke(HttpContext httpContext, ICurrentUserService currentUserService)
        {
            StringValues commandQuery;
            httpContext.Request.Headers.TryGetValue("command-id", out commandQuery);
            string commandId = commandQuery.FirstOrDefault();

            if( commandId == null || currentUserService.UserName == null )
            {
                await _next.Invoke(httpContext);
                return;
            }

            var userCommandStatus = _statusRepository.GetStatusOrDefault(currentUserService.UserName);

            if(userCommandStatus == null)
            {
                userCommandStatus = new UserCommandStatus { UserName = currentUserService.UserName };
            }
            else if(userCommandStatus.CommandId == commandId && userCommandStatus.HasSucceeded) //If duplicate successful request
            {      
                httpContext.Response.StatusCode = StatusCodes.Status200OK;
                var response = JsonSerializer.Serialize(new { isDuplicate = true });
                await httpContext.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(response));
                return;
            }

            await _next.Invoke(httpContext);

            var statusCode = httpContext.Response.StatusCode;

            userCommandStatus.HasSucceeded = statusCode >= 200 && statusCode <= 299;
            userCommandStatus.CommandId = commandId;

            await _statusRepository.SetStatusAsync(userCommandStatus);
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class OptimisticCommandMiddlewareExtensions
    {
        public static IApplicationBuilder UseOptimisticCommandMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptimisticCommandMiddleware>();
        }
    }
}
