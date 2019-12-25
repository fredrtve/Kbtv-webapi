using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Queries.UserQueries
{
    public class UserByUserNameResponseProfile : Profile
    {
        public UserByUserNameResponseProfile()
        {
            CreateMap<ApplicationUser, UserByUserNameResponse>()
                .ForMember(x => x.Role, opt => opt.Ignore());
        }
    }
}
