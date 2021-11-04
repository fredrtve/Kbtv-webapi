using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;
using BjBygg.Core;
using BjBygg.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public static class SyncQueryExtensions
    {
        public static async Task<SyncEntityResponse<TDto>> ToSyncResponseAsync<TEntity, TDto>(
            this IQueryable<TEntity> query, bool isInitial, IMapper mapper
        ) where TEntity : IDbSyncQueryResponse
        {
            var entities = await query.ToListAsync();

            if (entities.Count() == 0) return null;

            List<string> deletedEntities = new List<string>();

            if (!isInitial)
            {
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false).ToList(); //Remove deleted entities
            }

            return new SyncEntityResponse<TDto>(mapper.Map<IEnumerable<TDto>>(entities), deletedEntities);
        }

        public static IQueryable<T> GetSyncItems<T>(this IQueryable<T> query, DbSyncQuery request, bool getAllOnInitial = false) where T : BaseEntity
        {     
            if (getAllOnInitial && request.InitialSync) return query;

            if(request.InitialSync == false)
                query = query.IgnoreQueryFilters();

            if (request.Timestamp == null || request.Timestamp == 0) return query;

            var date = DateTimeHelper.ConvertEpochToDate((request.Timestamp / 1000) ?? 0);

            return query.Where(x => x.UpdatedAt >= date);
        }
    }
}
