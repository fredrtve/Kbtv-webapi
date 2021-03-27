using AutoMapper;
using BjBygg.Core;
using BjBygg.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public static class SyncQueryExtensions
    {
        public static IQueryable<TEntity> GetSyncItems<TEntity>(this IQueryable<TEntity> query, DbSyncQuery request, bool getAllOnInitial = false)
           where TEntity : BaseEntity
        {
            if (getAllOnInitial && request.InitialSync) return query;

            var date = DateTimeHelper.ConvertEpochToDate((request.Timestamp / 1000) ?? 0);

            if (!request.InitialSync)
                query = query.IgnoreQueryFilters(); //Include deleted property to check for deleted entities

            return query.GetEntitiesLaterThan(date);
        }

        public static IEnumerable<TEntity> GetChildSyncItems<TEntity>(this IEnumerable<TEntity> query, UserDbSyncQuery request)
            where TEntity : BaseEntity
        {
            if (request.InitialSync) return query;

            var date = DateTimeHelper.ConvertEpochToDate((request.Timestamp / 1000) ?? 0);

            return query.GetEntitiesLaterThan(date);
        }

        public static DbSyncArrayResponse<TDto> ToSyncArrayResponse<TEntity, TDto>(
            this IEnumerable<TEntity> entities, bool isInitial, IMapper mapper
        ) where TEntity : BaseEntity where TDto : DbSyncDto
        {
            if (!entities.Any()) return null;
            List<string> deletedEntities = new List<string>();

            if (!isInitial)
            {
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false); //Remove deleted entities
            }

            entities = entities.OrderByDescending(x => x.CreatedAt);

            return new DbSyncArrayResponse<TDto>(mapper.Map<IEnumerable<TDto>>(entities), deletedEntities);
        }

        private static IQueryable<T> GetEntitiesLaterThan<T>(this IQueryable<T> query, DateTime date) where T : BaseEntity
        {
            return query.Where(x => DateTime.Compare(x.UpdatedAt, date) >= 0);
        }
        private static IEnumerable<T> GetEntitiesLaterThan<T>(this IEnumerable<T> query, DateTime date) where T : BaseEntity
        {
            return query.Where(x => DateTime.Compare(x.UpdatedAt, date) >= 0);
        }
    }
}
