using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.EmployerCommands.Update
{
    class UpdateEmployerCommandProfile : Profile
    {
        public UpdateEmployerCommandProfile()
        {
            CreateMap<UpdateEmployerCommand, Employer>();
        }
    }
}
