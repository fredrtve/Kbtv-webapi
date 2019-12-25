using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BjBygg.Application.Queries.UserQueries
{
    public class UserByUserNameHandler : IRequestHandler<UserByUserNameQuery, UserByUserNameResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserByUserNameHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserByUserNameResponse> Handle(UserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            var response = _mapper.Map<UserByUserNameResponse>(user);

            if(user != null)
                response.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            
            return response;
        }
    }
}
