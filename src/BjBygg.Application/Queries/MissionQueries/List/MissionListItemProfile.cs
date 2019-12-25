using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.List
{
    public class MissionListItemProfile : Profile
    {
        public MissionListItemProfile()
        {
            CreateMap<Mission, MissionListItemDto>();
        }
    }
}
