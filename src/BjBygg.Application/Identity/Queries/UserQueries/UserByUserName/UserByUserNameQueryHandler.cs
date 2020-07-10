using AutoMapper;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Queries.UserQueries.UserByUserName
{
    public class UserByUserNameQueryHandler : IRequestHandler<UserByUserNameQuery, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserByUserNameQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new EntityNotFoundException(nameof(ApplicationUser), request.UserName);

            var response = _mapper.Map<UserDto>(user);

            response.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return response;
        }

    }
}
