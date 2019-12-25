using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.Detail
{
    public class MissionDetailByIdQuery : IRequest<MissionDetailByIdResponse>
    {
        public int Id { get; set; }
    }
}
