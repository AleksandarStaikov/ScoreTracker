using System.ComponentModel.DataAnnotations;

namespace ScoreTracker.WebApi.Models.Game;

public class CreateGameModel
{
    [Required]
    public string TeamId { get; set; } = string.Empty;

    [Required]
    public IEnumerable<int> RedPoints { get; set; } = Enumerable.Empty<int>();
    [Required]
    public IEnumerable<int> BlackPoints { get; set; } = Enumerable.Empty<int>();
}