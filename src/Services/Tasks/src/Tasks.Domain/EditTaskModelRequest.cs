namespace Tasks.Domain;

public class EditTaskModelRequest
{
    public Guid OwnerId { get; set; }
    public string Content { get; set; } = null!;
    public bool Finished { get; set; }
}