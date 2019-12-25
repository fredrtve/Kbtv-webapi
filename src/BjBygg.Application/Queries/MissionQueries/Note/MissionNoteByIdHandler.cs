using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace BjBygg.Application.Queries.MissionQueries.Note
{
    public class MissionNoteByIdHandler : IRequestHandler<MissionNoteByIdQuery, MissionNoteByIdResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionNoteByIdHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionNoteByIdResponse> Handle(MissionNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var note = await _dbContext.Set<MissionNote>().FindAsync(request.Id);
            return _mapper.Map<MissionNoteByIdResponse>(note);
        }
    }
}
