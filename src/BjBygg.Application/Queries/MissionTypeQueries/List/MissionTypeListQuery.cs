using BjBygg.Application.Common;
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
