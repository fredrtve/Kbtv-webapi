using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.UserCommands.Update
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        [Required]
        [Display(Name = "Brukernavn")]
        public string UserName { get; set; }

        [Display(Name = "Fornavn")]
        [StringLength(45, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string FirstName { get; set; }

        [Display(Name = "Etternavn")]
        [StringLength(45, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string LastName { get; set; }

        [Display(Name = "Mobil")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(12, ErrorMessage = "{0} må være mellom {2} og {1} tegn.", MinimumLength = 4)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Epost")]
        [EmailAddress(ErrorMessage = "Epost adressen er ikke gyldig.")]
        public string Email { get; set; }

        [Display(Name = "Rolle")]
        public string Role { get; set; }

        [Display(Name = "Oppdragsgiver")]
        public int? EmployerId { get; set; }

    }
}
