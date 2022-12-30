﻿using Identity.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Domain;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
}