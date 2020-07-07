using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.EmployerCommands.Update
{
    public class UpdateEmployerCommand : UpdateCommand<EmployerDto>
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
    public class UpdateEmployerCommandHandler : UpdateCommandHandler<Employer, UpdateEmployerCommand, EmployerDto>
    {
        public UpdateEmployerCommandHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
