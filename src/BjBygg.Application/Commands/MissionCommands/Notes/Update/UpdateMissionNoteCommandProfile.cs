using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Update
{
    class UpdateMissionNoteCommandProfile : Profile
    {
        public UpdateMissionNoteCommandProfile()
        {
            CreateMap<UpdateMissionNoteCommand, MissionNote>();
        }
    }
}
