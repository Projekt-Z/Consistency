namespace Web.Domain.DTOs.Tasks;

public class AddTaskRequestDto
{
    public Guid OwnerId { get; set; }
    public string Content { get; set; } = null!;   
}