using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Update
{
    public class UpdateMissionNoteHandler : IRequestHandler<UpdateMissionNoteCommand, MissionNoteDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMissionNoteHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionNoteDto> Handle(UpdateMissionNoteCommand request, CancellationToken cancellationToken)
        {
            var note = _mapper.Map<MissionNote>(request);

            try
            {
                _dbContext.Entry(note).State = EntityState.Modified;

                _dbContext.Entry(note).Property(x => x.MissionId).IsModified = false;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");
            }
            
            return _mapper.Map<MissionNoteDto>(note);
        }
    }
}
