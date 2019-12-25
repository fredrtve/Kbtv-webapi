using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.EmployerCommands.Delete
{
    public class DeleteEmployerHandler : IRequestHandler<DeleteEmployerCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteEmployerHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteEmployerCommand request, CancellationToken cancellationToken)
        {
            var employer = await _dbContext.Set<Employer>().FindAsync(request.Id);
            if (employer == null) return false;

            _dbContext.Set<Employer>().Remove(employer);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
