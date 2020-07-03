using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.EmployerCommands.Update
{
    public class UpdateEmployerHandler : UpdateCommandHandler<Employer, UpdateEmployerCommand, EmployerDto>
    {
        public UpdateEmployerHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) {}
    }
}
