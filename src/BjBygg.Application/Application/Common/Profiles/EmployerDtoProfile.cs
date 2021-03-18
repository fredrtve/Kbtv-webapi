using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
{
    public class EmployerDtoProfile : Profile
    {
        public EmployerDtoProfile()
        {
            CreateMap<Employer, EmployerDto>();
            CreateMap<EmployerDto, Employer>();
        }
    }
}
