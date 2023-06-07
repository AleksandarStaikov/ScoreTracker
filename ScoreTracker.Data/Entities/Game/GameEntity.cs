using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ScoreTracker.Data.Entities.Game;

public class GameEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string GameId { get; set; } = string.Empty;
    public string TeamId { get; set; } = string.Empty;

    public int TotalRedPoints { get; set; }
    public int TotalBlackPoints { get; set; }

    public IEnumerable<int> RedPoints { get; set; } = Enumerable.Empty<int>();
    public IEnumerable<int> BlackPoints { get; set; } = Enumerable.Empty<int>();
}