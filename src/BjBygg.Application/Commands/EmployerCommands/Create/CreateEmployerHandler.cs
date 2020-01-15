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
    public class CreateEmployerHandler : IRequestHandler<CreateEmployerCommand, int>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateEmployerHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEmployerCommand request, CancellationToken cancellationToken)
        {
            var employer = _mapper.Map<Employer>(request);

            _dbContext.Set<Employer>()
                .Add(employer);

            await _dbContext.SaveChangesAsync();
            return employer.Id;
        }
    }
}
