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

namespace BjBygg.Application.Queries.MissionQueries.List
{
    public class MissionListHandler : IRequestHandler<MissionListQuery, MissionListResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionListHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionListResponse> Handle(MissionListQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Mission> itemsOnPage;

            var query = _dbContext.Set<Mission>().AsQueryable();     

            if (!String.IsNullOrEmpty(request.SearchString))   
                query = query.Where(x => x.Address.Contains(request.SearchString));

            int totalItems = await query.CountAsync();

            itemsOnPage = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(request.ItemsPerPage * request.PageIndex)
                .Take(request.ItemsPerPage)
                .ToListAsync();

            return new MissionListResponse()
            {
               Missions = _mapper.Map<List<MissionListItemDto>>(itemsOnPage),
               PaginationInfo = new PaginationDto()
               {
                   ActualPage = request.PageIndex,
                   ItemsPerPage = itemsOnPage.Count,
                   TotalItems = totalItems,
                   TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / request.ItemsPerPage)).ToString())
               }            
            };
        }
    }

 
}
