using CleanArchitecture.Core.Entities;
using BjBygg.Application.Commands.Shared.DeleteRange;
using CleanArchitecture.Infrastructure.Data;


namespace BjBygg.Application.Commands.MissionReportTypeCommands.DeleteRange
{
    public class DeleteRangeMissionReportTypeHandler : DeleteRangeHandler<MissionReportType, DeleteRangeMissionReportTypeCommand>
    {
        public DeleteRangeMissionReportTypeHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
