using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionReportTypeQueries.List
{
    public class MissionReportTypeListQuery : IRequest<IEnumerable<MissionReportTypeDto>>
    {

    }
}
