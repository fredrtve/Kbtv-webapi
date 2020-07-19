using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserCommands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Not allowing new leaders
            if (request.Role.ToLower() == Roles.Leader) throw new ForbiddenException();

            if (!Roles.All.Contains(request.Role))
                throw new EntityNotFoundException(nameof(IdentityRole), request.Role);

            var user = _mapper.Map<ApplicationUser>(request);

            if (request.Role != Roles.Employer) user.EmployerId = null;

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new BadRequestException("Something went wrong when trying to create user");

            await _userManager.AddToRoleAsync(user, request.Role);

            var response = _mapper.Map<UserDto>(user);
            response.Role = request.Role;

            return response;
        }
    }
}
