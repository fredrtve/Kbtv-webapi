using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BjBygg.Application.Queries.UserQueries.RoleList
{
    public class RoleListHandler : IRequestHandler<RoleListQuery, IEnumerable<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleListHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
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
