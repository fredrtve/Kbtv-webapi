using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Create
{
    public class CreateMissionNoteHandler : IRequestHandler<CreateMissionNoteCommand, int>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMissionNoteHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMissionNoteCommand request, CancellationToken cancellationToken)
        {
            var note = _mapper.Map<MissionNote>(request);

            _dbContext.Set<MissionNote>()
                .Add(note);

            await _dbContext.SaveChangesAsync();
            
            return note.Id;
        }
    }
}
