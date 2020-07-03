using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.EmployerCommands.Create
{
    public class CreateEmployerHandler : CreateHandler<Employer, CreateEmployerCommand, EmployerDto>
    {
        public CreateEmployerHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) {}
    }
}
