using LunaEdgeServiceLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LunaEdgeServiceLayer.Data
{
	public class AppDBContext : DbContext
	{
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Models.Task> Tasks { get; set; }

		protected readonly IConfiguration configuration;

		public AppDBContext(DbContextOptions<AppDBContext> options, IConfiguration configuration) : base(options)
		{
			this.configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			string connectionString = configuration.GetConnectionString("AppDbConnectionString");

			if (!string.IsNullOrWhiteSpace(connectionString))
			{
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.HasMany(x => x.Tasks).WithOne(x => x.User).HasForeignKey(x => x.UserId);
				entity.HasIndex(x => x.Username).IsUnique();
				entity.HasIndex(x => x.Email).IsUnique();
				entity.ToTable("Users");
			});

			modelBuilder.Entity<Models.Task>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.ToTable("Tasks");
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
