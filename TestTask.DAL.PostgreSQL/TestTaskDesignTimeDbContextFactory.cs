using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestTask.DAL.PostgreSQL;

public class TestTaskDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestTaskDbContext>
{
    private const string ConnectionString = "User ID=postgres;Password=12345678;Host=localhost;Port=5432;Database=WeatherViewerDb;";

    public TestTaskDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TestTaskDbContext>();
        optionsBuilder.UseNpgsql(ConnectionString);
        return new TestTaskDbContext(optionsBuilder.Options);
    }
}