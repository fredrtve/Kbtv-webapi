using BjBygg.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Queries
{
    public class InboundEmailPasswordListQuery : IRequest<IEnumerable<InboundEmailPasswordDto>>
    {
    }
}
