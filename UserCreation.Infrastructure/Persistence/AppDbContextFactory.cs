using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserCreation.Infrastructure.Persistence;

/// <summary>
/// Factory for creating DbContext instances at design time (for migrations)
/// This is only used by EF Core CLI tools (dotnet ef migrations, etc.)
/// At runtime, the DbContext is created through Dependency Injection in DependencyInjection.cs
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Build configuration to read appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Get connection string from configuration (same as runtime)
        var connectionString = configuration.GetConnectionString("Default");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'Default' not found. Make sure appsettings.json exists in the current directory.");
        }

        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
