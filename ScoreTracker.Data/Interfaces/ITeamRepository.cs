using MongoDB.Driver;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Data.Entities.Team;

namespace ScoreTracker.Data.Interfaces;

public interface ITeamRepository
{
    Task<Team?> GetMatchingTeam(Team team);
    Task<Team> CreateTeam(Team team);
    Task<Team?> GetTeamByTeamId(string teamId);
    Task AddGameToTeam(string teamId, GameOverview gameOverview);


   Task<Team>? GetTeamWithPredicateAndProjection(string teamId, int projectionSize);

   Task<Team>? GetTeamWithPredicate(string teamId);

   Task<Team>? GetTeamWithFilterAndProjection(string teamId, int projectionSize);

    Task<Team>? GetTeamWithFilter(string teamId);
}