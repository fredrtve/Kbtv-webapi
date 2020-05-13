using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var dbNote = await _dbContext.MissionNotes.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbNote == null)
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            foreach (var property in request.GetType().GetProperties())
            {
                if (property.Name == "Id") continue;
                dbNote.GetType().GetProperty(property.Name).SetValue(dbNote, property.GetValue(request), null);
            }


            try{await _dbContext.SaveChangesAsync();}
            catch (Exception ex)
            {
                throw new BadRequestException($"Something went wrong when storing your request");
            }
            
            return _mapper.Map<MissionNoteDto>(dbNote);
        }
    }
}
