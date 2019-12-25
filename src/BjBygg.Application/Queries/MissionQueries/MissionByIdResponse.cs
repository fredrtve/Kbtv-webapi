using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries
{
    public class MissionByIdResponse
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }

        public int? MissionTypeId { get; set; }

        public int? EmployerId { get; set; }
    }
}
