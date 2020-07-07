using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.EmployerCommands.Create
{
    public class CreateEmployerCommand : CreateCommand<EmployerDto>
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
    public class CreateEmployerCommandHandler : CreateCommandHandler<Employer, CreateEmployerCommand, EmployerDto>
    {
        public CreateEmployerCommandHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
