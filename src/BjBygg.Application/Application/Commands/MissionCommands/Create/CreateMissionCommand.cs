using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.SharedKernel;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommand : CreateCommand, IContactable, IAddress
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string EmployerId { get; set; }
        public EmployerDto? Employer { get; set; }
        public List<MissionActivityDto>? MissionActivities { get; set; }
    }

    public class EmployerDto
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
