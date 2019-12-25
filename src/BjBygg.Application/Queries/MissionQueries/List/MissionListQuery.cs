using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.List
{
    public class MissionListQuery : IRequest<MissionListResponse>
    {
        public int PageIndex { get; set; }

        public int ItemsPerPage { get; set; }

        public string? SearchString { get; set; }
    }
}
