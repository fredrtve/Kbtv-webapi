using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.List
{
    public class MissionListItemDto
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
