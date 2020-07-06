using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Common
{
    public class MissionTypeDto : DbSyncDto
    {
        public int? Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string? Name { get; set; }
    }
}
