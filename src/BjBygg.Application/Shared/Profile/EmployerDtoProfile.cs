using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Shared
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
