using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeHandler : IRequestHandler<CreateDocumentTypeCommand, DocumentTypeDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDocumentTypeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DocumentTypeDto> Handle(CreateDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            var documentType = new DocumentType() { Name = request.Name };
            _dbContext.Set<DocumentType>().Add(documentType);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<DocumentTypeDto>(documentType);
        }
    }
}
