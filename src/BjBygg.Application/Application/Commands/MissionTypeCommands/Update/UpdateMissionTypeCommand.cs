using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands.Update
{
    public class UpdateMissionTypeCommand : UpdateCommand, IName
    {
        public string Name { get; set; }
    }
    public class UpdateMissionTypeCommandHandler : UpdateCommandHandler<MissionType, UpdateMissionTypeCommand>
    {
        public UpdateMissionTypeCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
