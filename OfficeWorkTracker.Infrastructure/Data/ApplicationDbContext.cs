using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using OfficeWorkTracker.Domain.Entities;
using System.Reflection.Emit;

namespace OfficeWorkTracker.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
		{

		}

		public DbSet<User> Users => Set<User>();
		public DbSet<TaskItem> Tasks { get; set; }
		public DbSet<TaskComment> TaskComments { get; set; }

		public DbSet<TaskActivity> TaskActivities { get; set; }

        public DbSet<UserRefereshToken> UserRefereshTokens { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<TimeEntryBreak> TimeEntryBreaks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<TaskActivity>()
                .HasOne(t => t.Task)
                .WithMany(x => x.Activities)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskActivity>()
               .HasOne(t => t.User)
               .WithMany(x => x.Activities)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TimeEntry>()
                .HasOne(t => t.User)
                .WithMany(u => u.TimeEntries)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TimeEntryBreak>()
                .HasOne(x => x.TimeEntry)
                .WithMany(x => x.Breaks)
                .HasForeignKey(x => x.TimeEntryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}