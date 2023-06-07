namespace ScoreTracker.Common.Models.Team;

public class TeamOverview
{
    public string TeamId { get; set; } = string.Empty;
    public string ClubsUsername { get; set; } = string.Empty;
    public string DiamodsUsername { get; set; } = string.Empty;
    public string HeartsUsername { get; set; } = string.Empty;
    public string SpadesUsername { get; set; } = string.Empty;

    public uint RedWins { get; set; }
    public uint BlackWins { get; set; }
}