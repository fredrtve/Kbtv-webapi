using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IAppDbContext : IDbContext
    {
        DbSet<Employer> Employers { get; set; }
        DbSet<EmployerUser> EmployerUsers { get; set; }
        DbSet<Activity> Activities { get; set; }
        DbSet<Mission> Missions { get; set; }
        DbSet<MissionActivity> MissionActivities { get; set; }
        DbSet<MissionType> MissionTypes { get; set; }
        DbSet<MissionImage> MissionImages { get; set; }
        DbSet<MissionDocument> MissionDocuments { get; set; }
        DbSet<MissionNote> MissionNotes { get; set; }
        DbSet<Timesheet> Timesheets { get; set; }
        DbSet<UserCommandStatus> UserCommandStatuses { get; set; }
        Task<LeaderSettings> GetLeaderSettingsAsync();

    }
}
