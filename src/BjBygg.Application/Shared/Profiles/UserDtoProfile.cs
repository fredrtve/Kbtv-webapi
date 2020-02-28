using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Shared
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
