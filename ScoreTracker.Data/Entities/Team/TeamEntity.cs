using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ScoreTracker.Data.Entities.Game;

namespace ScoreTracker.Data.Entities.Team;

public class TeamEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TeamId { get; set; } = string.Empty;

    public string ClubsUsername { get; set; } = string.Empty;
    public string DiamodsUsername { get; set; } = string.Empty;
    public string HeartsUsername { get; set; } = string.Empty;
    public string SpadesUsername { get; set; } = string.Empty;

    public uint RedWins { get; set; }
    public uint BlackWins { get; set; }

    public IEnumerable<GameOverviewEntity> Games { get; set; } = Enumerable.Empty<GameOverviewEntity>();
}