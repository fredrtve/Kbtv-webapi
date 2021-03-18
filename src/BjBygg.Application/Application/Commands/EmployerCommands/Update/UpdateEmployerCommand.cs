using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;

namespace BjBygg.Application.Application.Commands.EmployerCommands.Update
{
    public class UpdateEmployerCommand : UpdateCommand, IContactable, IAddress, IName
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
    public class UpdateEmployerCommandHandler : UpdateCommandHandler<Employer, UpdateEmployerCommand>
    {
        public UpdateEmployerCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
