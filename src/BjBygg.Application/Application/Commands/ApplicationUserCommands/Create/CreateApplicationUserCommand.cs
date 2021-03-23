using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Commands.UserCommands.Create;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.UserCommands.Create
{
    public class CreateApplicationUserCommand : CreateUserCommand, IOptimisticCommand, IContactable
    {
        public string? EmployerId { get; set; }
    }
    public class CreateApplicationUserCommandHandler : IRequestHandler<CreateApplicationUserCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAppDbContext _dbContext;

        public CreateApplicationUserCommandHandler(
            IMediator mediator, 
            IMapper mapper, 
            IAppDbContext dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Role == Roles.Employer) 
            {
                var employer = await _dbContext.Employers.FindAsync(request.EmployerId);

                if (employer == null)
                            throw new EntityNotFoundException(nameof(Employer), request.EmployerId);

                _dbContext.EmployerUsers.Add(_mapper.Map<EmployerUser>(request));
            }

            await _mediator.Send(_mapper.Map<CreateUserCommand>(request));
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }

}
