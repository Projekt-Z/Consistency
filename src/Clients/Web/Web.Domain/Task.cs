namespace Web.Domain;

public class Task
{
    public string Id { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; }
    public string? LastEdited { get; set; } = null;
    public bool Finished { get; set; }
    public DateTime? Deadline { get; set; }
    public bool Repeating { get; set; }
    public List<int>? Days { get; set; }
    public Guid OwnerId { get; set; }
}