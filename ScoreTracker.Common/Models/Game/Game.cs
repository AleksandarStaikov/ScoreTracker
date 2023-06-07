namespace ScoreTracker.Common.Models.Game;

public class Game
{
    public string GameId { get; set; } = string.Empty;
    public string TeamId { get; set; } = string.Empty;

    public int TotalRedPoints { get; set; }
    public int TotalBlackPoints { get; set; }

    public IEnumerable<int> RedPoints { get; set; } = Enumerable.Empty<int>();
    public IEnumerable<int> BlackPoints { get; set; } = Enumerable.Empty<int>();
}