using Microsoft.EntityFrameworkCore;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.DAL.PostgreSQL;

public class TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : DbContext(options)
{
	public DbSet<WeatherArchive> WeatherArchives { get; init; }
	
	public DbSet<WeatherRecord> WeatherRecords { get; init; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<WeatherRecord>(e =>
		{
			e.HasKey(i => i.Id);
			
			e.Property(i => i.WindDirection)
				.IsRequired()
				.HasConversion(
					i => i.ToString(), 
					i => Enum.Parse<WindDirections>(i));

			e.HasOne(o => o.WeatherArchive)
				.WithMany(i => i.WeatherRecords)
				.HasForeignKey(i => i.WeatherArchiveId);
		});

		modelBuilder.Entity<WeatherArchive>(e =>
		{
			e.HasKey(i => i.Id);

			e.HasMany(i => i.WeatherRecords)
				.WithOne(i => i.WeatherArchive)
				.HasForeignKey(i => i.WeatherArchiveId);
		});
	}
}