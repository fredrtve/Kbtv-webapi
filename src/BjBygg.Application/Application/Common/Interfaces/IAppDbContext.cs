using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IAppDbContext : IDbContext
    {
        DbSet<Employer> Employers { get; set; }
        DbSet<Mission> Missions { get; set; }
        DbSet<MissionType> MissionTypes { get; set; }
        DbSet<MissionImage> MissionImages { get; set; }
        DbSet<MissionDocument> MissionDocuments { get; set; }
        DbSet<MissionNote> MissionNotes { get; set; }
        DbSet<Timesheet> Timesheets { get; set; }
    }
}
