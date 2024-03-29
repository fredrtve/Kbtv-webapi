﻿using BjBygg.Application.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Queries.TimesheetQueries
{
    public class TimesheetQuery : IRequest<List<TimesheetDto>>
    {
        public string? MissionId { get; set; }
        public string? ActivityId { get; set; }

        public long? StartDate { get; set; }

        public long? EndDate { get; set; }

        public string? UserName { get; set; }


    }
}
