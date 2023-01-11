﻿using Identity.Api.Domain.Models;
using Identity.Domain.DTOs;

namespace Identity.Application;

public interface IUserRepository
{
    Task Add(CreateUserRequest request);
    User Get();
    Task<List<User>> GetAll();
    bool Delete();
}