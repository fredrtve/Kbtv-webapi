using BjBygg.Application.Commands.Shared.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;


namespace BjBygg.Application.Commands.MissionCommands.Reports.DeleteRange
{
    public class DeleteRangeMissionReportHandler : DeleteRangeHandler<MissionReport, DeleteRangeMissionReportCommand>
    {
        public DeleteRangeMissionReportHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
