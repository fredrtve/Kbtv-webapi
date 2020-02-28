using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllQuery: IRequest<SyncAllResponse>
    {
        public string FromDate { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
