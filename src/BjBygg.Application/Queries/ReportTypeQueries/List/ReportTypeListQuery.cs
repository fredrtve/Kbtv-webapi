using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.ReportTypeQueries.List
{
    public class ReportTypeListQuery : IRequest<IEnumerable<ReportTypeDto>>
    {

    }
}
