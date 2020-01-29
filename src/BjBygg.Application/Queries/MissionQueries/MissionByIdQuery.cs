using BjBygg.Application.Shared;
using MediatR;

namespace BjBygg.Application.Queries.MissionQueries
{
    public class MissionByIdQuery : IRequest<MissionDto>
    {
        public int Id { get; set; }
    }
}
