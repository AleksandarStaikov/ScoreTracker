using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;

namespace ScoreTracker.Data.Interfaces;

public interface ITeamRepository
{
    Task<Team?> GetMatchingTeam(Team team);
    Task<Team> CreateTeam(Team team);
    Task<Team?> GetTeamByTeamId(string teamId);
    Task AddGameToTeam(string teamId, GameOverview gameOverview);
}