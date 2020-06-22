using BjBygg.Application.Shared.Dto;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Queries
{
    public class InboundEmailPasswordListHandler : IRequestHandler<InboundEmailPasswordListQuery, IEnumerable<InboundEmailPasswordDto>>
    {
        private readonly AppIdentityDbContext _dbContext;

        public InboundEmailPasswordListHandler(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IEnumerable<InboundEmailPasswordDto>> Handle(InboundEmailPasswordListQuery request, CancellationToken cancellationToken)
        {
            var passwords = await _dbContext.InboundEmailPasswords.Select(x => new InboundEmailPasswordDto()
            {
                Id = x.Id,
                Password = x.Password
            }).ToListAsync();

            return passwords;
        }
    }
}
