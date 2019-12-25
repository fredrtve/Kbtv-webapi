using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.List
{
    public class MissionListResponse
    {
        public List<MissionListItemDto> Missions { get; set; }

        public PaginationDto PaginationInfo { get; set; }
    }

}
