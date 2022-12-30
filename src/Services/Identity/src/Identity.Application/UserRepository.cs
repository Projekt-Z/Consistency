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
    
    public async Task Add(CreateUserRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = request.Password,
            CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds().ToString()
        };
        
        user.ProvideSaltAndHash();
        
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();
    }

    public User Get()
    {
        throw new NotImplementedException();
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