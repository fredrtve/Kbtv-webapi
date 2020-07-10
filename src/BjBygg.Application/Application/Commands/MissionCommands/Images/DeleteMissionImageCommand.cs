using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images
{
    public class DeleteMissionImageCommand : DeleteCommand
    {
    }
    public class DeleteMissionImageCommandHandler : DeleteCommandHandler<MissionImage, DeleteMissionImageCommand>
    {
        public DeleteMissionImageCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
