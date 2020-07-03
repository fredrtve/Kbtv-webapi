using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Images.Delete
{
    public class DeleteMissionImageHandler : DeleteHandler<MissionImage, DeleteMissionImageCommand>
    {
        public DeleteMissionImageHandler(AppDbContext dbContext) :
            base(dbContext) { }
    }
}
