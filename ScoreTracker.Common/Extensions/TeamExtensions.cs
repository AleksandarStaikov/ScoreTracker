using ScoreTracker.Common.Models.Team;

namespace ScoreTracker.Common.Extensions;

public static class TeamExtensions
{
    public static bool ContainsUser(this Team team, string username)
    {
        var userPartOfTeam =
           username == team.SpadesUsername ||
           username == team.DiamodsUsername ||
           username == team.HeartsUsername ||
           username == team.ClubsUsername;

        return userPartOfTeam;
    }

    public static bool IsUserRedTeam(this Team team, string username)
    {
        var isUserRedTeam =
           username == team.DiamodsUsername ||
           username == team.HeartsUsername;

        return isUserRedTeam;
    }

    public static bool IsUserBlackTeam(this Team team, string username)
    {
        var isUserBlackTeam =
           username == team.SpadesUsername ||
           username == team.ClubsUsername;

        return isUserBlackTeam;
    }

    public static string[] GetUsernames(this Team team)
        => new[]
        {
            team.SpadesUsername,
            team.DiamodsUsername,
            team.HeartsUsername,
            team.ClubsUsername
        };

}