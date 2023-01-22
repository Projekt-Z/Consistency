using Identity.Api.Domain.Models;
using Identity.Domain;
using Identity.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<(bool, object)> Add(CreateUserRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = request.Password,
            CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds().ToString()
        };
        
        user.ProvideSaltAndHash();
        
        if (Get(request.Username) is not null) return (false, new { Message = "User with this username already exists." });
        
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return (true, user);
    }

    public User? Get(Guid id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    private User? Get(string username)
    {
        return _context.Users.FirstOrDefault(x => x.Username == username);
    }
    
    public Task<List<User>> GetAll()
    {
        return _context.Users.ToListAsync();
    }

    public bool Delete()
    {
        throw new NotImplementedException();
    }
}