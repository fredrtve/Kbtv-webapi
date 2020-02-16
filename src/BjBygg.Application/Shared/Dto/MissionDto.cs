using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    public class MissionDto
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public bool Finished { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public MissionTypeDto MissionType { get; set; }

        public EmployerDto Employer { get; set; }
    }
}
