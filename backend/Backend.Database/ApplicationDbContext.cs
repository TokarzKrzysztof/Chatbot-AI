using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backend.Models.Entities;

namespace Backend.Database
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Message> Messages { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddDelegateDecompiler();
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Message>().Property(x => x.Reaction).HasConversion<string>();

			base.OnModelCreating(builder);
		}
	}
}
