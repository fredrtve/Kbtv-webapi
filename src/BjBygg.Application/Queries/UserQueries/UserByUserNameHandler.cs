using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;

namespace BjBygg.Application.Queries.UserQueries
{
    public class UserByUserNameHandler : IRequestHandler<UserByUserNameQuery, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserByUserNameHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UserByUserNameQuery request, CancellationToken cancellationToken)
        {
           var user = await _userManager.FindByNameAsync(request.UserName);

           if (user == null) throw new EntityNotFoundException($"User does not exist with username {request.UserName}");

            var response = _mapper.Map<UserDto>(user);
           response.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
           return response;
        }                             
        
    }
}
