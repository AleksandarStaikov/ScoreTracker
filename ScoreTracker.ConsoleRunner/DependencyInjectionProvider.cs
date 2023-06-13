using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.ConsoleRunner.Imports;
using ScoreTracker.Data;

namespace ScoreTracker.ConsoleRunner;

public class DependencyInjectionProvider
{
    public static IServiceProvider GetServiceProvider()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.Development.json", false)
       .Build();

        var services = new ServiceCollection()
            .AddTransient<IDataSeeder, DataSeeder>()
            .AddData(configuration)
            .AddScoreTracker(configuration)
            .AddLogging();

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
