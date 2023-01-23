namespace Web.Domain.DTOs;

public class SignInResponseDto
{
    public string Token { get; set; } = null!;
    public int Expires { get; set; }
}