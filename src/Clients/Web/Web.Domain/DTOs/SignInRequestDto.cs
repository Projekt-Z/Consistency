namespace Web.Domain.DTOs;

public class SignInRequestDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}