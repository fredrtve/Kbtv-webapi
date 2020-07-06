using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Queries.DbSyncQueries.DocumentTypeQuery
{
    public class DocumentTypeSyncHandler : BaseDbSyncHandler<DocumentTypeSyncQuery, DocumentType, DocumentTypeDto>
    {
        public DocumentTypeSyncHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false){}
    }
}