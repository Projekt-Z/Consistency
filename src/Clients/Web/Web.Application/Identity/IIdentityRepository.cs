using Microsoft.AspNetCore.Components.Authorization;
using Web.Domain;
using Web.Domain.DTOs;

namespace Web.Application.Identity;

public interface IIdentityRepository
{ 
    Task<IEnumerable<User>?> GetAllUsers();
    Task<(bool, object)> RegisterUser(RegisterUserDto userDto);
    Task<(bool, object)> LoginUser(SignInRequestDto userDto);
}