using AutoMapper;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.EmployerCommands.Create
{
    class CreateEmployerProfile : Profile
    {
        public CreateEmployerProfile()
        {
            CreateMap<CreateEmployerCommand, Employer>();
        }
    }
}
