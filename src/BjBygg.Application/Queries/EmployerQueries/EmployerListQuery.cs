using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.EmployerQueries.List
{
    public class EmployerListQuery : DateRangeQuery, IRequest<IEnumerable<EmployerDto>>
    {

    }
}
