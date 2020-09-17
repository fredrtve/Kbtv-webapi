using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.Behaviours
{
    public class OptimisticCommandBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserSerice;

        public OptimisticCommandBehaviour(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserSerice)
        {
            _userManager = userManager;
            _currentUserSerice = currentUserSerice;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        { 
            bool isOptimistic = request is IOptimisticCommand;
            try
            {
                var response = await next();
                if (isOptimistic) await updateOptimisticCommandStatusAsync(true);
                return response;
            }
            catch (Exception ex)
            {
                if (isOptimistic) await updateOptimisticCommandStatusAsync(false);
                throw;
            }
        }

        private async Task updateOptimisticCommandStatusAsync(bool status)
        {
            if (_currentUserSerice.UserName == null) return;
            var user = await _userManager.FindByNameAsync(_currentUserSerice.UserName);
            user.LastCommandStatus = status;
            await _userManager.UpdateAsync(user);
        }
    }
}
