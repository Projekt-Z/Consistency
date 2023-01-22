using Identity.Api.Domain.Models;
using Identity.Domain.DTOs;

namespace Identity.Application;

public interface IUserRepository
{
    Task<(bool, object)> Add(CreateUserRequest request);
    User? Get(Guid id);
    Task<List<User>> GetAll();
    bool Delete();
}