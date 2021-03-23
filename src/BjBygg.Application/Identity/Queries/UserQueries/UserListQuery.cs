using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BjBygg.Application.Identity.Queries.UserQueries
{
    public class UserListQuery : IRequest<List<UserDto>> { }

    public class UserListHandler : IRequestHandler<UserListQuery, List<UserDto>>
    {
        private readonly IAppIdentityDbContext _dbContext;

        public UserListHandler(IAppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public Task<List<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var usersWithRoles = (from user in _dbContext.Users
                                  select new
                                  {
                                      user.UserName,
                                      user.FirstName,
                                      user.LastName,
                                      user.PhoneNumber,
                                      user.Email,
                                      Role = (from userRole in user.Roles
                                              join role in _dbContext.Roles on userRole.RoleId
                                              equals role.Id
                                              select role.Name).ToList()
                                  }).ToList().Select(p => new UserDto()

                                  {
                                      UserName = p.UserName,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      PhoneNumber = p.PhoneNumber,
                                      Email = p.Email,
                                      Role = p.Role.FirstOrDefault()
                                  }).ToList();

            return Task.FromResult(usersWithRoles);
        }
    }
}
