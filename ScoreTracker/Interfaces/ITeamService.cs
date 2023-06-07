using ScoreTracker.Common.Models.Team;

namespace ScoreTracker.Interfaces;

/// <summary>
/// Defines logic for managing the teams
/// </summary>
public interface ITeamService
{
    Task<string> CreateTeam(CreateTeamDto createTeamDto);
    Task<Team?> GetTeamByTeamId(string teamId, string userAuthId);
}