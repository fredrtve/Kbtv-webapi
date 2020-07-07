using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Create
{
    class CreateMissionNoteCommandProfile : Profile
    {
        public CreateMissionNoteCommandProfile()
        {
            CreateMap<CreateMissionNoteCommand, MissionNote>();
        }
    }
}
