using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.UserCommands.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Role == "Leder") return false; //Not allowing new leaders

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) return false;
       
            if(!String.IsNullOrEmpty(request.FirstName)) 
                user.FirstName = request.FirstName;

            if (!String.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;

            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;

            var result = await _userManager.UpdateAsync(user);

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (!result.Succeeded) return false;

            if (currentRole != request.Role && currentRole != "Leder" && !String.IsNullOrEmpty(request.Role))
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole);
                await _userManager.AddToRoleAsync(user, request.Role);
                return true;
            }
            
            return true;
        }
    }
}
