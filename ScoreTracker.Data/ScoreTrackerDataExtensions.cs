using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ScoreTracker.Data.Interfaces;
using ScoreTracker.Data.Repositories;
using System.Security.Authentication;

namespace ScoreTracker.Data;

public static class ScoreTrackerDataExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<ITeamRepository, TeamRepository>()
            .AddTransient<IGameRepository, GameRepository>()
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddMongoIntegration(configuration);


    public static IServiceCollection AddMongoIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        var configSettings = configuration
            .GetSection(nameof(DatabaseSettings))
            .Get<DatabaseSettings>();

        if (configSettings == null)
        {
            throw new InvalidOperationException($"Missing configuration section '{nameof(DatabaseSettings)}'");
        }

        MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(
          new MongoUrl(configSettings.ConnectionString)
        );
        mongoSettings.SslSettings =
          new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
        var mongoClient = new MongoClient(mongoSettings);
        var mongoDB = mongoClient.GetDatabase(configSettings.DatabaseName);

        services.AddSingleton(mongoDB);

        return services;
    }
}
