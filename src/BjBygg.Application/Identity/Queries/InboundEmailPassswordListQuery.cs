using BjBygg.Application.Identity.Common;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Identity.Queries
{
    public class InboundEmailPasswordListQuery : IRequest<List<InboundEmailPasswordDto>>
    {
    }
}
