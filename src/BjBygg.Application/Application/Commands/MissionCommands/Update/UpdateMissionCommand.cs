using BjBygg.Application.Common.BaseEntityCommands.Update;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommand : UpdateCommand, IContactable, IAddress
    {
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public bool? Finished { get; set; }
        public MissionTypeDto MissionType { get; set; }
        public string? MissionTypeId { get; set; }
        public EmployerDto Employer { get; set; }
        public string? EmployerId { get; set; }
        public Position? Position { get; set; }
        public List<MissionActivityDto>? MissionActivities { get; set; }
    }

    public class EmployerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class MissionTypeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class MissionActivityDto
    {
        public string Id { get; set; }
        public string? ActivityId { get; set; }
        public ActivityDto? Activity { get; set; }
    }

    public class ActivityDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
