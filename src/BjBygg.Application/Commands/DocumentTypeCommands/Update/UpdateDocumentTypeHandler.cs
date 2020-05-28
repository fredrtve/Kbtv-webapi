using AutoMapper;
using BjBygg.Application.Commands.DocumentTypeCommands.Update;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Update
{
    public class UpdateDocumentTypeHandler : IRequestHandler<UpdateDocumentTypeCommand, DocumentTypeDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateDocumentTypeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DocumentTypeDto> Handle(UpdateDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            var dbDocumentType = await _dbContext.DocumentTypes.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbDocumentType == null)
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            foreach (var property in request.GetType().GetProperties())
            {
                if (property.Name == "Id") continue;
                dbDocumentType.GetType().GetProperty(property.Name).SetValue(dbDocumentType, property.GetValue(request), null);
            }


            try { await _dbContext.SaveChangesAsync(); }
            catch (Exception ex)
            {
                throw new BadRequestException($"Something went wrong when storing your request");
            }

            return _mapper.Map<DocumentTypeDto>(dbDocumentType);
        }
    }
}
