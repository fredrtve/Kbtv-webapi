using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.Note
{
    public class MissionNoteByIdQuery : IRequest<MissionNoteDetailsDto>
    {
        public int Id { get; set; }
    }
}
