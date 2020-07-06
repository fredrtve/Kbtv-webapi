using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.IdentityCommands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<UserDto>
    {
        [Required]
        public string? UserName { get; set; }

        [Display(Name = "Mobil")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(12, ErrorMessage = "{0} må være mellom {2} og {1} tegn.", MinimumLength = 4)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Epost")]
        [EmailAddress(ErrorMessage = "Epost adressen er ikke gyldig.")]
        public string Email { get; set; }

    }
}
