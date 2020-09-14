using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommandHandler : UpdateCommandHandler<Mission, UpdateMissionCommand>
    {
        public UpdateMissionCommandHandler(IAppDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper) {}

    }
}
