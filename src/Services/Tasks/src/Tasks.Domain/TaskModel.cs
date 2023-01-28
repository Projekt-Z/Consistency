using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tasks.Domain;

[BsonIgnoreExtraElements]
public class TaskModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastEdited { get; set; } = null;
    public bool Finished { get; set; }
    public DateTime? Deadline { get; set; }
    public bool Repeating { get; set; }
    public List<int>? Days { get; set; }
    public Guid OwnerId { get; set; }
}