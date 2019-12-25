using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.UserCommands.Create
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [Display(Name = "Brukernavn")]
        [StringLength(45, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} må fylles ut.")]
        [Display(Name = "Fornavn")]
        [StringLength(45, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} må fylles ut.")]
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
        [Required]
        public string Role { get; set; }

        [Display(Name = "Passord")]
        [Required]
        public string Password { get; set; }
    }
}
