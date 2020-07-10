using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class DocumentTypeSyncQuery : DbSyncQuery<DocumentTypeDto>
    {
    }

    public class DocumentTypeSyncQueryHandler : BaseDbSyncHandler<DocumentTypeSyncQuery, DocumentType, DocumentTypeDto>
    {
        public DocumentTypeSyncQueryHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false)
        { }
    }
}
