using AutoMapper;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UpdateProfileCommandHandler(
            UserManager<ApplicationUser> userManager,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_currentUserService.UserName);

            if (user == null)
                throw new EntityNotFoundException(nameof(ApplicationUser), _currentUserService.UserName);

            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new BadRequestException("Something went wrong when trying to update profile");

            var response = _mapper.Map<UserDto>(user);
            return response;
        }
    }
}
