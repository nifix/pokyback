using PokyBack.Shared.Core.Persistence;
using PokyBack.Shared.Infrastructure.Persistence;

namespace PokyBack;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("SqliteDev");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not find a connection string named 'DefaultConnection'.");
        }
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlite(
            connectionString,
            b => b.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name)
        ).UseLazyLoadingProxies();

        return new AppDbContext(optionsBuilder.Options, ModelBuilderAction);

        void ModelBuilderAction(ModelBuilder mb) =>
            mb.ApplyConfigurationsFromAssembly(typeof(RoomEntityTypeConfiguration).Assembly);
    }
}