using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ScoreTracker.Data.Entities.Team;

namespace ScoreTracker.Data.Entities.User;

public class UserEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public string AuthId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;


    public IEnumerable<UserOverviewEntity> Friends { get; set; } = Enumerable.Empty<UserOverviewEntity>();
    public IEnumerable<TeamOverviewEntity> Teams { get; set; } = Enumerable.Empty<TeamOverviewEntity>();
}