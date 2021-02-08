using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public static class SyncQueryExtensions
    {
        public static IQueryable<TEntity> GetSyncItems<TEntity, TDto>(this IQueryable<TEntity> query, DbSyncQuery<TDto> request, bool checkMinDate = true)
           where TEntity : BaseEntity where TDto : DbSyncDto
        {
            var date = DateTimeHelper.ConvertEpochToDate((request.Timestamp / 1000) ?? 0);

            if (checkMinDate)
                date = CheckMinSyncDate(date, request.InitialTimestamp);

            if (request.Timestamp != null)
                query = query.IgnoreQueryFilters(); //Include deleted property to check for deleted entities

            return GetEntitiesLaterThan(date, query);
        }

        public static IQueryable<TEntity> GetMissionChildSyncItems<TEntity, TDto>(this IQueryable<TEntity> query, UserDbSyncQuery<TDto> request)
            where TEntity : BaseEntity, IMissionChildEntity where TDto : DbSyncDto
        {
            query = query.AsQueryable().GetSyncItems(request)
                .Include(x => x.Mission).Where(x => !x.Mission.Deleted);

            if (request.User.Role == Roles.Employer) //Only allow employers missions if role is employer
                query = query.Where(x => (x.Mission.EmployerId == request.User.EmployerId));

            return query;
        }

        public async static Task<DbSyncArrayResponse<TDto>> ToSyncArrayResponseAsync<TEntity, TDto>(
            this IQueryable<TEntity> query, bool isInitial, IMapper mapper
        ) where TEntity : BaseEntity where TDto : DbSyncDto
        {
            var entities = await query.ToListAsync();
            List<string> deletedEntities = new List<string>();

            if (!isInitial)
            {
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false).ToList(); //Remove deleted entities
            }

            return new DbSyncArrayResponse<TDto>(mapper.Map<IEnumerable<TDto>>(entities), deletedEntities);
        }

        private static DateTime CheckMinSyncDate(DateTime date, long? InitialTimestamp)
        {
            DateTime minDate;

            if (InitialTimestamp != null)
                minDate = DateTimeHelper.ConvertEpochToDate((long)InitialTimestamp / 1000);
            else
                minDate = DateTimeHelper.Now().AddMonths(48);

            //If date is older than min date, return min date. 
            if (DateTime.Compare(date, minDate) < 0) return minDate;
            else return date;
        }

        private static IQueryable<T> GetEntitiesLaterThan<T>(DateTime date, IQueryable<T> query) where T : BaseEntity
        {
            return query.Where(x => DateTime.Compare(x.UpdatedAt, date) >= 0);
        }
    }
}
