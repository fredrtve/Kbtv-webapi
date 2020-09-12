using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionTypeDto : DbSyncDto
    {
        public string? Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string? Name { get; set; }
    }
}
