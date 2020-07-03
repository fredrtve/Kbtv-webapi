using BjBygg.Application.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public abstract class UserDbSyncQuery<T> : DbSyncQuery<T> where T : DbSyncDto
    {
        [Required]
        public UserDto User { get; set; }
    }
}
