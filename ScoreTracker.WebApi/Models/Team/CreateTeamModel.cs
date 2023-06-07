using System.ComponentModel.DataAnnotations;

namespace ScoreTracker.WebApi.Models.Team;

public class CreateTeamModel
{
    [Required]
    public string HeartsUsername { get; set; } = string.Empty;
    [Required]
    public string DiamondsUsername { get; set; } = string.Empty;
    [Required]
    public string SpadesUsername { get; set; } = string.Empty;
}