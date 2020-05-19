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

        public UserListHandler(UserManager<ApplicationUser> userManager, IMapper mapper, AppIdentityDbContext dbContext )
        {
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public Task<IEnumerable<UserListItemDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var usersWithRoles = (from user in _dbContext.Users
                                  select new
                                  {
                                      user.UserName,
                                      user.FirstName,
                                      user.LastName,
                                      user.PhoneNumber,
                                      user.EmployerId,
                                      Role = (from userRole in user.Roles
                                              join role in _dbContext.Roles on userRole.RoleId
                                              equals role.Id
                                              select role.Name).ToList()
                                  }).ToList().Select(p => new UserListItemDto()

                                  {
                                      UserName = p.UserName,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      PhoneNumber = p.PhoneNumber,
                                      EmployerId = p.EmployerId,
                                      Role = p.Role.FirstOrDefault()
                                  });

            return Task.FromResult(usersWithRoles);
        }
    }
}
