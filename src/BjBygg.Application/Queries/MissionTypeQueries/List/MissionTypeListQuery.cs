using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionTypeQueries.List
{
    public class MissionTypeListQuery : IRequest<IEnumerable<MissionTypeDto>>
    {

    }
}
