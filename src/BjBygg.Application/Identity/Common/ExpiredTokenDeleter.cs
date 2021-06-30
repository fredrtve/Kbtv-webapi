using BjBygg.Application.Identity.Commands.UserIdentityCommands.DeleteOldTokens;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Common
{
    public class ExpiredTokenDeleter : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public ExpiredTokenDeleter(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
                o => DeleteExpiredTokens(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromDays(14)
            );
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void DeleteExpiredTokens()
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new DeleteExpiredTokensCommand());
        }
    }
}


