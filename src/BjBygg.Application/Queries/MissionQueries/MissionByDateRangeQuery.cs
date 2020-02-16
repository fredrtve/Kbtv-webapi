using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries
{
    public class MissionByDateRangeQuery : DateRangeQuery, IRequest<IEnumerable<MissionDto>>
    {
    }
}
