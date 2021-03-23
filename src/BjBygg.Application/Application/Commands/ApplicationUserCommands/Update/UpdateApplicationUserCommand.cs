using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserCommands.Update;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.ApplicationUserCommands.Update
{
    public class UpdateApplicationUserCommand : UpdateUserCommand
    {
        public string? EmployerId { get; set; }
    }
    public class UpdateApplicationUserCommandHandler : IRequestHandler<UpdateApplicationUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAppDbContext _dbContext;
        private readonly IMediator _mediator;

        public UpdateApplicationUserCommandHandler(
            IMapper mapper, 
            IAppDbContext dbContext,
            IMediator mediator)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var employerUser = await _dbContext.EmployerUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            if (employerUser != null && request.Role != Roles.Employer)
                _dbContext.EmployerUsers.Remove(employerUser);

            if (request.Role == Roles.Employer && employerUser?.EmployerId != request.EmployerId)
            {
                if (employerUser != null) _dbContext.EmployerUsers.Remove(employerUser);

                var newEmployer = await _dbContext.Employers.FindAsync(request.EmployerId);

                if (newEmployer == null)
                    throw new EntityNotFoundException(nameof(Employer), request.EmployerId);
                var emUs = _mapper.Map<EmployerUser>(request);
                _dbContext.EmployerUsers.Add(emUs);
            }

            await _mediator.Send(_mapper.Map<UpdateUserCommand>(request));
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
