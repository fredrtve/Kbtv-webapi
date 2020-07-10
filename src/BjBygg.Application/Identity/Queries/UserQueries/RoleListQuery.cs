using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Queries.UserQueries
{
    public class RoleListQuery : IRequest<List<string>>
    {
    }

    public class RoleListQueryHandler : IRequestHandler<RoleListQuery, IEnumerable<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleListQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public Task<IEnumerable<string>> Handle(RoleListQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _roleManager.Roles.Select(x => x.Name).AsEnumerable();
            });
        }
    }
}
