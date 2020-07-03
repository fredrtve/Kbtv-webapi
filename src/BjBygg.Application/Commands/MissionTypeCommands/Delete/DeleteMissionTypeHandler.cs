using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionTypeCommands.Delete
{
    public class DeleteMissionTypeHandler : DeleteHandler<MissionType, DeleteMissionTypeCommand>
    {
        public DeleteMissionTypeHandler(AppDbContext dbContext) :
            base(dbContext) { }
    }
}
