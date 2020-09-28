using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Common
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(x => x.Role, opt => opt.Ignore());

            CreateMap<UserDto, ApplicationUser>()
                .ForMember(x => x.Role, opt => opt.Ignore());
        }
    }
}
