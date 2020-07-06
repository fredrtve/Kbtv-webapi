using BjBygg.Application.Common.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries
{
    public class InboundEmailPasswordListQuery : IRequest<IEnumerable<InboundEmailPasswordDto>>
    {
    }
}
