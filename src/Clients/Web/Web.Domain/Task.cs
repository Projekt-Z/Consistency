namespace Web.Domain;

public class Task
{
    public string Id { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; }
    public string? LastEdited { get; set; } = null;
    public bool Finished { get; set; }
    public Guid OwnerId { get; set; }
}