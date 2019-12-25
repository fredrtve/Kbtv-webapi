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

            int totalItems = await _dbContext.Set<Mission>().CountAsync();

            if (String.IsNullOrEmpty(request.SearchString))
            {
                itemsOnPage = await _dbContext.Set<Mission>().AsQueryable()
                    .OrderBy(x => x.CreatedAt)
                    .Skip(request.ItemsPerPage * request.PageIndex)
                    .Take(request.ItemsPerPage)
                    .ToListAsync();
            }
            else
            {
                var formatedSearchString = new CultureInfo("nb-NO", false).TextInfo.ToTitleCase(request.SearchString);
                itemsOnPage = await _dbContext.Set<Mission>().AsQueryable()
                    .Where(x => x.Address.Contains(request.SearchString))
                    .OrderBy(x => x.CreatedAt)
                    .Skip(request.ItemsPerPage * request.PageIndex)
                    .Take(request.ItemsPerPage)
                    .ToListAsync();           
            }

            var response = new MissionListResponse()
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

            return response;
        }
    }

 
}
