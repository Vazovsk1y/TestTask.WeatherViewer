using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Models;

namespace TestTask.DAL;

public class TestTaskSecondContext : DbContext
{
	public DbSet<WeatherRecord> WeatherRecords { get; set; }
	public TestTaskSecondContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		var weatherRecordBuilder = modelBuilder.Entity<WeatherRecord>();

		weatherRecordBuilder.HasKey(x => x.Id);

		weatherRecordBuilder.Property(e => e.MainWindDirection)
			.IsRequired()
			.HasConversion(e => e.ToString(), i => Enum.Parse<WindDirection>(i));

		weatherRecordBuilder.Property(e => e.SecondaryWindDirection)
			.IsRequired()
			.HasConversion(e => e.ToString(), i => Enum.Parse<WindDirection>(i));
	}
}
