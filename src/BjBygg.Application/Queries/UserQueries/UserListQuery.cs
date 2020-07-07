using BjBygg.Application.Common;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BjBygg.Application.Queries.UserQueries
{
    public class UserListQuery : IRequest<IEnumerable<UserDto>> { }

    public class UserListHandler : IRequestHandler<UserListQuery, IEnumerable<UserDto>>
    {
        private readonly AppIdentityDbContext _dbContext;

        public UserListHandler(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public Task<IEnumerable<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var usersWithRoles = (from user in _dbContext.Users
                                  select new
                                  {
                                      user.UserName,
                                      user.FirstName,
                                      user.LastName,
                                      user.PhoneNumber,
                                      user.Email,
                                      user.EmployerId,
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
                                      EmployerId = p.EmployerId,
                                      Role = p.Role.FirstOrDefault()
                                  });

            return Task.FromResult(usersWithRoles);
        }
    }
}
