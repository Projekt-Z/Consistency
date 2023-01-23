namespace Web.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string Salt { get; set; } = null!;
}