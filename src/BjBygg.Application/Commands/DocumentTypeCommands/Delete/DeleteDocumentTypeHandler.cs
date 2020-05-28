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

namespace BjBygg.Application.Commands.DocumentTypeCommands.Delete
{
    public class DeleteDocumentTypeHandler : IRequestHandler<DeleteDocumentTypeCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteDocumentTypeHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            var documentType = await _dbContext.Set<DocumentType>().FindAsync(request.Id);
            if (documentType == null) throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            _dbContext.Set<DocumentType>().Remove(documentType);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
