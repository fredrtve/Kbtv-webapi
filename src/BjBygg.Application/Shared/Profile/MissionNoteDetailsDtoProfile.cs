using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.Note
{
    public class MissionNoteDetailsDtoProfile : Profile
    {
        public MissionNoteDetailsDtoProfile()
        {
            CreateMap<MissionNote, MissionNoteDetailsDto>();
            CreateMap<MissionNoteDetailsDto, MissionNote>();
        }
    }
}
