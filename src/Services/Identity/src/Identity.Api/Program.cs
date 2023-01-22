using Identity.Api.Domain.Models;
using Identity.Application;
using Identity.Domain;
using Identity.Domain.DTOs;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var settings = new Settings();
builder.Configuration.Bind("Settings", settings);
builder.Services.AddSingleton(settings);

builder.Services.AddDbContext<ApplicationContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGet("/auth", ([FromServices] IUserRepository userRepo) => userRepo.GetAll());
}

app.MapPut("/auth", async (CreateUserRequest request, [FromServices] IUserRepository userRepo) =>
{
    var success = userRepo.Add(request);

    await success;

    if (!success.Result.Item1) return Results.BadRequest(success.Result.Item2);
    
    var usr = success.Result.Item2 as User;
    return Results.Created($"/auth/{usr!.Id}", null);
});

app.MapPost("/auth", (LoginUserRequest request, [FromServices] ApplicationContext db) =>
{
    var user = db.Users.SingleOrDefault(x => x.Username == request.Username);

    if (user is null) return Results.NotFound(new {Message = $"User with {request.Username} does not exists"});
    
    if (user.PasswordHash != AuthenticationHelper.GenerateHash(request.Password, user.Salt))
        return Results.BadRequest(new {Message = "Password is not valid"});

    var token = AuthenticationHelper.GenerateJwt(AuthenticationHelper.AssembleClaimsIdentity(user), settings);
    
    return Results.Ok(new
    {
        Token = token,
        Expires = DateTimeOffset.Now.AddDays(7).ToUnixTimeSeconds()
    });
});

app.Run();