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
    }
}