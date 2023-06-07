namespace ScoreTracker.Common.Models.Game;

public class CreateGameDto
{
    public string TeamId { get; set; } = string.Empty;
    public string CurrentUserAuthId { get; set; } = string.Empty;

    public IEnumerable<int> RedPoints { get; set; } = Enumerable.Empty<int>();
    public IEnumerable<int> BlackPoints { get; set; } = Enumerable.Empty<int>();
}
