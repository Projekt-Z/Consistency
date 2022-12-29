namespace Tasks.Api.Domain;

public class CreateTaskModelRequest
{
    public Guid OwnerId { get; set; }
    public string Content { get; set; } = null!;
}