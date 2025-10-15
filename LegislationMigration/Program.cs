using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LegislationMigration.Data;
using LegislationMigration.Services.Interfaces;
using LegislationMigration.Services.Implementations;
using LegislationMigration.Repositories.Interfaces;
using LegislationMigration.Repositories.Implementations;

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
                services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("OldConnection")));

                // Add HTTP client
                services.AddHttpClient();

                // Dependency Injection
                services.AddScoped<IReprocessService, LegislationReprocessService>();
                services.AddScoped<IExtractService, ExtractService>();
                services.AddScoped<IJobStatusService, JobStatusService>();
                services.AddScoped<ILegislationRepository, LegislationRepository>();

                services.AddLogging();
            })
            .Build();

        // Run the actual process
        using var scope = host.Services.CreateScope();
        var reprocessor = scope.ServiceProvider.GetRequiredService<IReprocessService>();

        //Console.WriteLine("Enter PDF folder path:");
        var folderPath = "E:\\Prem\\pdfs_tables (2)";

        Console.WriteLine("Enter language (en/ar):");
        var language = Console.ReadLine() ?? "en";

        await reprocessor.ReprocessLegislationAsync(folderPath, language);

        Console.WriteLine("✅ Reprocessing Completed.");
    }
}
