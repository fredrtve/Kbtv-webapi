using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Delete
{
    public class DeleteMissionNoteHandler : IRequestHandler<DeleteMissionNoteCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteMissionNoteHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteMissionNoteCommand request, CancellationToken cancellationToken)
        {
            var missionNote = await _dbContext.Set<MissionNote>().FindAsync(request.Id);

            if (missionNote == null) throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            _dbContext.Set<MissionNote>().Remove(missionNote);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
