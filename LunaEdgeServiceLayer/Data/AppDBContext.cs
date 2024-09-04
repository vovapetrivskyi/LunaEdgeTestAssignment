using LunaEdgeServiceLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeServiceLayer.Data
{
	public class AppDBContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<LunaEdgeServiceLayer.Data.Models.Task> Tasks { get; set; }

		protected readonly IConfiguration configuration;

		public AppDBContext(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			string connectionString = configuration.GetConnectionString("AppDbConnectionString");
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
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

			modelBuilder.Entity<LunaEdgeServiceLayer.Data.Models.Task>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.ToTable("Tasks");
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
