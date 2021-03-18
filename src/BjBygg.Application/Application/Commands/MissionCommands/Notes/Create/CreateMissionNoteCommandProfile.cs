using AutoMapper;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes.Create
{
    class CreateMissionNoteCommandProfile : Profile
    {
        public CreateMissionNoteCommandProfile()
        {
            CreateMap<CreateMissionNoteCommand, MissionNote>();
        }
    }
}
