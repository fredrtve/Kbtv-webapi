using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using System;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionDto : DbSyncDto
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public bool Finished { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }

        public int MissionTypeId { get; set; }

        public int EmployerId { get; set; }

        public MissionTypeDto MissionType { get; set; }

        public EmployerDto Employer { get; set; }

        public Uri? ImageURL { get; set; }
    }
}
