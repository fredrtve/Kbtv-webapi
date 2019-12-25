using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionTypeQueries
{ 
    public class MissionTypeListItemProfile : Profile
    {
        public MissionTypeListItemProfile()
        {
            CreateMap<MissionType, MissionTypeListItemDto>();
        }
    }
}
