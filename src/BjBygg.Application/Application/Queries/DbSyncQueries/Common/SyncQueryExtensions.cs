using AutoMapper;
using BjBygg.Core;
using BjBygg.Core.Interfaces;
using BjBygg.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public static class SyncQueryExtensions
    {
        public static IQueryable<TEntity> GetSyncItems<TEntity>(this IQueryable<TEntity> query, DbSyncQuery request, bool checkMinDate = true)
           where TEntity : BaseEntity
        {
            var date = DateTimeHelper.ConvertEpochToDate((request.Timestamp / 1000) ?? 0);

            if (checkMinDate)
                date = CheckMinSyncDate(date, request.InitialTimestamp);

            if (request.Timestamp != null)
                query = query.IgnoreQueryFilters(); //Include deleted property to check for deleted entities

            return query.GetEntitiesLaterThan(date);
        }

        public static IEnumerable<TEntity> GetMissionChildSyncItems<TEntity>(this IEnumerable<TEntity> query, UserDbSyncQuery request)
            where TEntity : BaseEntity, IMissionChildEntity
        {
            if (request.Timestamp == null) return query;

            var date = DateTimeHelper.ConvertEpochToDate((request.Timestamp / 1000) ?? 0);

            date = CheckMinSyncDate(date, request.InitialTimestamp);

            return query.GetEntitiesLaterThan(date);
        }

        public static DbSyncArrayResponse<TDto> ToSyncArrayResponse<TEntity, TDto>(
            this IEnumerable<TEntity> entities, bool isInitial, IMapper mapper
        ) where TEntity : BaseEntity where TDto : DbSyncDto
        {
            List<string> deletedEntities = new List<string>();

            if (!isInitial)
            {
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false); //Remove deleted entities
            }

            entities = entities.OrderByDescending(x => x.CreatedAt);

            return new DbSyncArrayResponse<TDto>(mapper.Map<IEnumerable<TDto>>(entities), deletedEntities);
        }

        private static DateTime CheckMinSyncDate(DateTime date, long? InitialTimestamp)
        {
            DateTime minDate;

            if (InitialTimestamp != null)
                minDate = DateTimeHelper.ConvertEpochToDate((long)InitialTimestamp / 1000);
            else
                minDate = DateTimeHelper.Now().AddMonths(-48);

            //If date is older than min date, return min date. 
            if (DateTime.Compare(date, minDate) < 0) return minDate;
            else return date;
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
