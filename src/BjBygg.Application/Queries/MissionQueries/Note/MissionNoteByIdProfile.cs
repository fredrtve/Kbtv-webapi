using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.Note
{
    public class MissionNoteByIdProfile : Profile
    {
        public MissionNoteByIdProfile()
        {
            CreateMap<MissionNote, MissionNoteByIdResponse>();
        }
    }
}
