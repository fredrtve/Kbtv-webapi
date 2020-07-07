using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Images
{
    public class DeleteMissionImageCommand : DeleteCommand
    {
    }
    public class DeleteMissionImageCommandHandler : DeleteCommandHandler<MissionImage, DeleteMissionImageCommand>
    {
        public DeleteMissionImageCommandHandler(AppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
