using LegislationMigration.Data;
using LegislationMigration.Services.Implementations;
using LegislationMigration.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    public static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                // Add DbContext
                services.AddDbContextFactory<NewDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                // Add HTTP client
                services.AddHttpClient();

                // Dependency Injection
                services.AddScoped<IReprocessService, LegislationReprocessService>();
                services.AddScoped<IExtractService, ExtractService>();
                services.AddScoped<IJobStatusService, JobStatusService>();

                services.AddLogging();
            })
            .Build();

        // Run the actual process
        using var scope = host.Services.CreateScope();
        var reprocessor = scope.ServiceProvider.GetRequiredService<IReprocessService>();

        //Console.WriteLine("Enter PDF folder path:");
        var folderPath = "E:\\Prem\\pdfs_tables (2)";

        await reprocessor.ReprocessLegislationAsync(folderPath);

        Console.WriteLine("✅ Reprocessing Completed.");
    }
}
