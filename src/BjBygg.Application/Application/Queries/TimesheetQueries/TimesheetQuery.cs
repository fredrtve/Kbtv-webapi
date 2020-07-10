using BjBygg.Application.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Queries.TimesheetQueries
{
    public class TimesheetQuery : IRequest<List<TimesheetDto>>
    {
        public int? MissionId { get; set; }

        public double? StartDate { get; set; }

        public double? EndDate { get; set; }

        public string? UserName { get; set; }


    }
}
