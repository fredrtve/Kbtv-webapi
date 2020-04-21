using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.TimesheetQueries
{
    public class TimesheetQuery : IRequest<IEnumerable<TimesheetDto>>
    {
        public int? MissionId { get; set; }

        public double? StartDate { get; set; }

        public double? EndDate { get; set; }

        public string? UserName { get; set; }


    }
}
