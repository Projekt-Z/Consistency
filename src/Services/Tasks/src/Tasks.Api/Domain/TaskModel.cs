namespace Tasks.Api.Domain;

public class TaskModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastEdited { get; set; } = null;
    public bool Finished { get; set; }
    public Guid OwnerId { get; set; }
}