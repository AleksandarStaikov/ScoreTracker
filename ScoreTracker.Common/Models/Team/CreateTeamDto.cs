namespace ScoreTracker.Common.Models.Team;

public class CreateTeamDto
{
    public string CurrentUserAuthId { get; set; } = string.Empty;
    public string HeartsUsername { get; set; } = string.Empty;
    public string DiamondsUsername { get; set; } = string.Empty;
    public string SpadesUsername { get; set; } = string.Empty;
}