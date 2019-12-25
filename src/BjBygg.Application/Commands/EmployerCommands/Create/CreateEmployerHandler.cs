using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.EmployerCommands.Create
{
    public class CreateEmployerHandler : IRequestHandler<CreateEmployerCommand>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateEmployerHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateEmployerCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Set<Employer>()
                .Add(_mapper.Map<Employer>(request));

            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
