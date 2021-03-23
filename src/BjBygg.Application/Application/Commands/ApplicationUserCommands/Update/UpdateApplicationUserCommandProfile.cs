using AutoMapper;
using BjBygg.Application.Identity.Commands.UserCommands.Update;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.ApplicationUserCommands.Update
{
    class UpdateApplicationUserCommandProfile : Profile
    {
        public UpdateApplicationUserCommandProfile()
        {
            CreateMap<UpdateApplicationUserCommand, UpdateUserCommand>()
                .ForSourceMember(x => x.EmployerId, opt => opt.DoNotValidate());

            CreateMap<UpdateApplicationUserCommand, EmployerUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.EmployerId, opt => opt.MapFrom(x => x.EmployerId))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
