using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class DocumentTypeSyncQuery : DbSyncQuery<DocumentTypeDto>
    {
    }

    public class DocumentTypeSyncQueryHandler : BaseDbSyncHandler<DocumentTypeSyncQuery, DocumentType, DocumentTypeDto>
    {
        public DocumentTypeSyncQueryHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false)
        { }
    }
}
