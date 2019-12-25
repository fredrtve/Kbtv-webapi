using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries
{
    public class MissionByIdProfile : Profile
    {
        public MissionByIdProfile()
        {
            CreateMap<Mission, MissionByIdResponse>();
        }
    }
}
