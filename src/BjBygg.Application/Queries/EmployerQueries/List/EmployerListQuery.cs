using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.EmployerQueries.List
{
    public class EmployerListQuery : IRequest<IEnumerable<EmployerListItemDto>>
    {

    }
}
