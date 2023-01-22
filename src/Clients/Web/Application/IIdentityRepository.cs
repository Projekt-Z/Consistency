using Domain;
using Domain.DTOs;

namespace Application;

public interface IIdentityRepository
{ 
    Task<IEnumerable<User>?> GetAllUsers();
    Task<(bool, object)> RegisterUser(RegisterUserDto userDto);
    Task<(bool, object)> LoginUser(SignInRequestDto userDto);
}