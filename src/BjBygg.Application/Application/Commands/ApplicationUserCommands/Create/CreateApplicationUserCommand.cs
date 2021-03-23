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
        private readonly IIdGenerator _idGenerator;
        private readonly IMapper _mapper;
        private readonly IAppDbContext _dbContext;

        public CreateApplicationUserCommandHandler(
            IMediator mediator, 
            IIdGenerator idGenerator,
            IMapper mapper, 
            IAppDbContext dbContext)
        {
            _mediator = mediator;
            _idGenerator = idGenerator;
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

                var employerUser = _mapper.Map<EmployerUser>(request);
                employerUser.Id = _idGenerator.Generate();
                _dbContext.EmployerUsers.Add(employerUser);
            }

            await _mediator.Send(_mapper.Map<CreateUserCommand>(request));
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }

}
