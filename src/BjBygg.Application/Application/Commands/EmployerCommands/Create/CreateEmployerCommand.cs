using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;

namespace BjBygg.Application.Application.Commands.EmployerCommands.Create
{
    public class CreateEmployerCommand : CreateCommand, IContactable, IAddress, IName
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
    public class CreateEmployerCommandHandler : CreateCommandHandler<Employer, CreateEmployerCommand>
    {
        public CreateEmployerCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
