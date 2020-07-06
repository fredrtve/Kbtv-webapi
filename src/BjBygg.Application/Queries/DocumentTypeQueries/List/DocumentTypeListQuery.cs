using BjBygg.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DocumentTypeQueries.List
{
    public class DocumentTypeListQuery : IRequest<IEnumerable<DocumentTypeDto>>
    {

    }
}
