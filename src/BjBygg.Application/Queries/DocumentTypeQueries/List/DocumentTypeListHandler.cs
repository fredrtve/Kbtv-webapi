using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BjBygg.Application.Shared;

namespace BjBygg.Application.Queries.DocumentTypeQueries.List
{
    public class DocumentTypeListHandler : IRequestHandler<DocumentTypeListQuery, IEnumerable<DocumentTypeDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public DocumentTypeListHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentTypeDto>> Handle(DocumentTypeListQuery request, CancellationToken cancellationToken)
        {
            var DocumentTypes = await _dbContext.Set<DocumentType>().ToListAsync();

            return DocumentTypes.Select(x => _mapper.Map<DocumentTypeDto>(x));
        }
    }

 
}
