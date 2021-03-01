using System;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;

namespace BjBygg.WebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class OptimisticCommandMiddleware
    {
        private readonly RequestDelegate _next;

        public OptimisticCommandMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            StringValues commandQuery;
            httpContext.Request.Headers.TryGetValue("command-id", out commandQuery);
            string commandId = commandQuery.FirstOrDefault();

            await _next.Invoke(httpContext);

            if (commandId == null) return;

            var statusCode = httpContext.Response.StatusCode;

            if (statusCode >= 200 && statusCode <= 299)
                await updateUserCommandStatusAsync(commandId, true, userManager, currentUserService);
            else
                await updateUserCommandStatusAsync(commandId, false, userManager, currentUserService);
        }

        private async Task updateUserCommandStatusAsync(string commandId, bool status, UserManager<ApplicationUser> userManager, ICurrentUserService currentUserSerice)
        {
            if (currentUserSerice.UserName == null) return;
            var user = await userManager.FindByNameAsync(currentUserSerice.UserName);
            user.LastCommandStatus = status;
            user.LastCommandId = commandId;
            await userManager.UpdateAsync(user);
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
