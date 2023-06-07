namespace ScoreTracker.Common.Models.Game;

public class GameOverview
{
    public string GameId { get; set; } = string.Empty;

    public int TotalRedPoints { get; set; }
    public int TotalBlackPoints { get; set; }
}