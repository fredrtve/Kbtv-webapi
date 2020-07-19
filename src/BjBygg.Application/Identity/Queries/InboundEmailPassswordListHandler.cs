using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Queries
{
    public class InboundEmailPasswordListHandler : IRequestHandler<InboundEmailPasswordListQuery, List<InboundEmailPasswordDto>>
    {
        private readonly IAppIdentityDbContext _dbContext;

        public InboundEmailPasswordListHandler(IAppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<List<InboundEmailPasswordDto>> Handle(InboundEmailPasswordListQuery request, CancellationToken cancellationToken)
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
