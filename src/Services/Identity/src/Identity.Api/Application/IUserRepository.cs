using Identity.Api.Domain.DTOs;
using Identity.Api.Domain.Models;

namespace Identity.Api.Application;

public interface IUserRepository
{
    Task Add(CreateUserRequest request);
    User Get();
    Task<List<User>> GetAll();
    bool Delete();
}