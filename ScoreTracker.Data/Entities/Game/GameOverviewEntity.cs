namespace ScoreTracker.Data.Entities.Game;

public class GameOverviewEntity
{
    public string GameId { get; set; } = string.Empty;

    public int TotalRedPoints { get; set; }
    public int TotalBlackPoints { get; set; }
}