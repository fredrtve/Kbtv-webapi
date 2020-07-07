using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.IdentityCommands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;

        public UpdateProfileCommandHandler(
            UserManager<ApplicationUser> userManager,
            ICurrentUserService userService,
            IMapper mapper)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_userService.UserName);

            if (user == null)
                throw new EntityNotFoundException($"User does not exist with username {_userService.UserName}"); ;

            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.ToString());

            var response = _mapper.Map<UserDto>(user);
            return response;
        }
    }
}
