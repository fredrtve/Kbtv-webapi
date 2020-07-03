using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Create
{
    class CreateMissionNoteProfile : Profile
    {
        public CreateMissionNoteProfile()
        {
            CreateMap<CreateMissionNoteCommand, MissionNote>();
        }
    }
}
