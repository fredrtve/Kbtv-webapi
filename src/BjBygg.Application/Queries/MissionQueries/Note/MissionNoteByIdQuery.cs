using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.Note
{
    public class MissionNoteByIdQuery : IRequest<MissionNoteByIdResponse>
    {
        public int Id { get; set; }
    }
}
