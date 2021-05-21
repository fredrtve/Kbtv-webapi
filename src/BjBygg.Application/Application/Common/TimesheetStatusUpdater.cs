using BjBygg.Application.Application.Commands.TimesheetCommands;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Common
{
    public class TimesheetStatusUpdater : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public TimesheetStatusUpdater(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
                o => UpdateMonthlyStatuses(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(12)
            ); 
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void UpdateMonthlyStatuses()
        {
            var now = DateTimeHelper.NowLocalTime();

            //if (now.Day != 1) return;

            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new ConfirmAllTimesheetsLastMonthCommand());
            }      
        }
    }
}
