using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.Interfaces;
using ScoreTracker.Services;

namespace ScoreTracker;

/// <summary>
/// Provides dependensy registration functionality
/// </summary>
public static class ScoreTrackerExtensions
{

    /// <summary>
    /// Register all needed dependencies
    /// </summary>
    public static IServiceCollection AddScoreTracker(this IServiceCollection servies, IConfigurationRoot configurationRoot)
    {
        servies.AddTransient<IUserService, UserService>();
        servies.AddTransient<ITeamService, TeamService>();
        servies.AddTransient<IGameService, GameService>();

        return servies;
    }
}