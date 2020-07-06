using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Common
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
