using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.EmployerQueries.List
{
    public class EmployerListItemProfile : Profile
    {
        public EmployerListItemProfile()
        {
            CreateMap<Employer, EmployerListItemDto>();
        }
    }
}
