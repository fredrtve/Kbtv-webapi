using Ardalis.EFCore.Extensions;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionType> MissionTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<MissionImage> MissionImages { get; set; }
        public DbSet<MissionDocument> MissionDocuments { get; set; }
        public DbSet<MissionNote> MissionNotes { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var now = DateTime.UtcNow;
            var user = _currentUserService.UserName;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ITrackable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedAt = now;
                            trackable.UpdatedBy = user;
                            entry.Property("CreatedBy").IsModified = false;
                            entry.Property("CreatedAt").IsModified = false;
                            break;
                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.UpdatedAt = now;
                            trackable.CreatedBy = user;
                            trackable.Deleted = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            trackable.Deleted = true;
                            trackable.UpdatedAt = now;
                            break;
                    }
                }
            }
        }
    }
}
