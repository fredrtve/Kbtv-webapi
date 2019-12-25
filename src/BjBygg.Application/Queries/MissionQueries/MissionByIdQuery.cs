using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries
{
    public class MissionByIdQuery : IRequest<MissionByIdResponse>
    {
        public int Id { get; set; }
    }
}
