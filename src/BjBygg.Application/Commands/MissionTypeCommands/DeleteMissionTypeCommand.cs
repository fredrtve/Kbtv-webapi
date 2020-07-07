
using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionTypeCommands
{
    public class DeleteMissionTypeCommand : DeleteCommand
    {
    }
    public class DeleteMissionTypeCommandHandler : DeleteCommandHandler<MissionType, DeleteMissionTypeCommand>
    {
        public DeleteMissionTypeCommandHandler(AppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
