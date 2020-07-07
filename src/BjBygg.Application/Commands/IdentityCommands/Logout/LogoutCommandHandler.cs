using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.IdentityCommands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppIdentityDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public LogoutCommandHandler(
            UserManager<ApplicationUser> userManager,
            AppIdentityDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.UserName == _currentUserService.UserName);

            if (user == null) throw new BadRequestException("Cant find user");

            user.RemoveRefreshToken(request.RefreshToken);

            try { await _dbContext.SaveChangesAsync(); }
            catch (Exception ex)
            {
                throw new BadRequestException($"Something went wrong while updating");
            }

            return Unit.Value;
        }
    }
}
