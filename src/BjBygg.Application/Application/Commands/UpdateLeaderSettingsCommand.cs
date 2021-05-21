using BjBygg.Application.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands
{
    public class UpdateLeaderSettingsCommand : IRequest
    {
        public bool ConfirmTimesheetsMonthly { get; set; }
    }

    public class UpdateLeaderSettingsHandler : IRequestHandler<UpdateLeaderSettingsCommand>
    {
        private readonly IAppDbContext _dbContext;

        public UpdateLeaderSettingsHandler(IAppDbContext dbContext) {
           _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateLeaderSettingsCommand request, CancellationToken cancellationToken)
        {
            var dbSettings = await _dbContext.GetLeaderSettingsAsync();
            dbSettings.ConfirmTimesheetsMonthly = request.ConfirmTimesheetsMonthly;
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}