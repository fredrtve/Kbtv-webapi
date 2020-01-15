using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;
using System.Linq;

namespace BjBygg.Application.Queries.UserQueries.List
{
    public class UserListItemProfile : Profile
    {
        public UserListItemProfile()
        {
            CreateMap<ApplicationUser, UserListItemDto>()
                .ForMember(
                    dest => dest.Role,
                    act => act.MapFrom(x => x.Roles.FirstOrDefault()));
        }
    }
}
