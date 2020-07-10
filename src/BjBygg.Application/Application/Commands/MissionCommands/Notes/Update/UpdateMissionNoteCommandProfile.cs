using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes.Update
{
    class UpdateMissionNoteCommandProfile : Profile
    {
        public UpdateMissionNoteCommandProfile()
        {
            CreateMap<UpdateMissionNoteCommand, MissionNote>();
        }
    }
}
