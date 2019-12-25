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
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace BjBygg.Application.Queries.UserQueries.List
{
    public class UserListHandler : IRequestHandler<UserListQuery, IEnumerable<UserListItemDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly AppIdentityDbContext _dbContext;

        public UserListHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserListItemDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            if (!String.IsNullOrEmpty(request.Role))
                return _mapper.Map<IEnumerable<UserListItemDto>>(await _userManager.GetUsersInRoleAsync(request.Role));

            return _mapper.Map<IEnumerable<UserListItemDto>>(await _userManager.Users.ToListAsync());
        }
    }
}
