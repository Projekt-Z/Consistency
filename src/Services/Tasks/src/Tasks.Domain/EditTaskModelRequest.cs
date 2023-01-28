namespace Tasks.Domain;

public class EditTaskModelRequest
{
    public Guid OwnerId { get; set; }
    public string Content { get; set; } = null!;
    public bool Finished { get; set; }
    public DateTime? Deadline { get; set; }
    public List<int>? Days { get; set; }
    public override string ToString()
    {
        return $"{OwnerId} - {Content} - {Finished}";
    }
}