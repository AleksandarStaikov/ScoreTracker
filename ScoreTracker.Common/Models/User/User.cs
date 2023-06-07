using ScoreTracker.Common.Models.Team;

namespace ScoreTracker.Common.Models.User;

public class User
{
    public string Id { get; set; } = string.Empty;
    public string AuthId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public IEnumerable<UserOverview> Friends { get; set; } = Enumerable.Empty<UserOverview>();
    public IEnumerable<TeamOverview> Teams { get; set; } = Enumerable.Empty<TeamOverview>();
}