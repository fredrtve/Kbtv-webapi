using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
