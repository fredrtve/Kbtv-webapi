using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Entities;
using System;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionDto : DbSyncDto
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public bool Finished { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }

        public string MissionTypeId { get; set; }

        public string EmployerId { get; set; }

        public MissionTypeDto MissionType { get; set; }

        public EmployerDto Employer { get; set; }

        public Uri? FileName { get; set; }

        public Position Position { get; set; }
    }
}
