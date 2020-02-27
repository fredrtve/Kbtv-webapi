using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.SharedKernel;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public abstract class DbSyncHandler<T, Q, R> : IRequestHandler<Q, DbSyncResponse<R>>
        where T : BaseEntity where Q : DbSyncQuery<R> where R : DbSyncDto
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public DbSyncHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DbSyncResponse<R>> Handle(Q request, CancellationToken cancellationToken)
        {
            List<T> entities;
            List<int> deletedEntities = new List<int>();

            var query = _dbContext.Set<T>();
            var minDate = DateTime.Now.AddYears(-5);
            var date = DateTime.MinValue;
        
            DateTime.TryParseExact(request.FromDate, "o", null, System.Globalization.DateTimeStyles.None, out date);
            if (DateTime.Compare(date, minDate) < 0) //Initial call to populate cache
            {
                entities = await query
                    .Where(x => DateTime.Compare(x.CreatedAt, minDate) > 0)
                    .ToListAsync();
            }
            else
            {       
                entities = await query
                    .IgnoreQueryFilters() //Include deleted property to check for deleted entities
                    .Where(x => DateTime.Compare(x.UpdatedAt, date) > 0)
                    .ToListAsync();
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false).ToList(); //Remove deleted entities
            }


            return new DbSyncResponse<R>() {
                Entities = _mapper.Map<IEnumerable<R>>(entities),
                DeletedEntities = deletedEntities,
                Timestamp = DateTime.Now.ToString("o"),
            };
        }
    }
}
