using AutoMapper;
using BjBygg.Application.Application.Commands.UserCommands.Create;
using BjBygg.Application.Identity.Commands.UserCommands.Create;
using BjBygg.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Application.Commands.ApplicationUserCommands
{
    class CreateApplicationUserCommandProfile : Profile
    {
        public CreateApplicationUserCommandProfile()
        {
            CreateMap<CreateApplicationUserCommand, CreateUserCommand>()
                .ForSourceMember(x => x.EmployerId, opt => opt.DoNotValidate());

            CreateMap<CreateApplicationUserCommand, EmployerUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.EmployerId, opt => opt.MapFrom(x => x.EmployerId))
                .ForAllOtherMembers(x => x.Ignore());
                
        }
    }
}
