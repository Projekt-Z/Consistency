namespace Web.Domain.DTOs;

public class MessageResponseDto
{
    public string Message { get; set; } = null!;

    public override string ToString()
    {
        return Message;
    }
}