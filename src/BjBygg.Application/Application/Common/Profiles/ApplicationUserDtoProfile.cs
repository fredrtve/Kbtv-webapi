using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Application.Common.Profiles
{
    public class ApplicationUserDtoProfile : Profile
    {
        public ApplicationUserDtoProfile()
        {
            CreateMap<ApplicationUserDto, UserDto>().ForSourceMember(x => x.EmployerId, v => v.DoNotValidate());
            CreateMap<UserDto, ApplicationUserDto>().ForMember(x => x.EmployerId, v => v.Ignore());

            CreateMap<ApplicationUser, ApplicationUserDto>().ForMember(x => x.EmployerId, v => v.Ignore());
            CreateMap<ApplicationUserDto, ApplicationUser>().ForSourceMember(x => x.EmployerId, v => v.DoNotValidate());
        }
    }
}
