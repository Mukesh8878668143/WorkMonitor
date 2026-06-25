using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using OfficeWorkTracker.Domain.Entities;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
        }
    }
}