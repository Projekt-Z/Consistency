using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Domain.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(50)]
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string Salt { get; set; } = null!;
}