using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Common.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserCommands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (ForbiddenRoles.Value.Contains(request.Role)) 
                throw new ForbiddenException();

            if (!Roles.All.Contains(request.Role))
                throw new EntityNotFoundException(nameof(IdentityRole), request.Role);

            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var validationErrors = result.Errors.Select(x => new ValidationFailure(x.Code, x.Description));
                throw new ValidationException(validationErrors);
            }

            await _userManager.AddToRoleAsync(user, request.Role);

            return Unit.Value;
        }
    }
}
