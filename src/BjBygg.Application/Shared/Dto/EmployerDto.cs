using System;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Shared
{
    public class EmployerDto
    {
        public int? Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string? Name { get; set; }

        [StringLength(12, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string? PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string? Address { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
