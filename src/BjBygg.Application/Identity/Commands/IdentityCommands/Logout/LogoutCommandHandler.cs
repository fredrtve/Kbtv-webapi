using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.IdentityCommands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppIdentityDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public LogoutCommandHandler(
            UserManager<ApplicationUser> userManager,
            IAppIdentityDbContext dbContext,
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

            if (user == null)
                throw new EntityNotFoundException(nameof(ApplicationUser), _currentUserService.UserName);

            user.RemoveRefreshToken(request.RefreshToken);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
