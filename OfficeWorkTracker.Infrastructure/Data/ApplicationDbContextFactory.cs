using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace OfficeWorkTracker.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<ApplicationDbContext>();

        // Read appsettings.json
        IConfigurationRoot configuration =
            new ConfigurationBuilder()
             .SetBasePath
             (
                Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "../OfficeWorkTracker.API")
                )
                .AddJsonFile("appsettings.json")
                .Build();
    

        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}