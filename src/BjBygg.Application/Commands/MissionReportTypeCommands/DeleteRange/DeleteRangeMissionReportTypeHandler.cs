using CleanArchitecture.Core.Entities;
using BjBygg.Application.Commands.Shared.DeleteRange;
using CleanArchitecture.Infrastructure.Data;


namespace BjBygg.Application.Commands.ReportTypeCommands.DeleteRange
{
    public class DeleteRangeReportTypeHandler : DeleteRangeHandler<ReportType, DeleteRangeReportTypeCommand>
    {
        public DeleteRangeReportTypeHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
