using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Tasks.Application;
using Tasks.Domain;

var builder = WebApplication.CreateBuilder(args);

var settings = new Settings();
builder.Configuration.Bind("Settings", settings);
builder.Services.AddSingleton(settings);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.BearerKey)),
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false
    });

var app = builder.Build();

var connectionString = app.Configuration.GetConnectionString("mongodb");
//var mongoUrl = MongoUrl.Create(connectionString);
var mongoClient = new MongoClient(connectionString);
var db = mongoClient.GetDatabase("ctaskdb");

var taskService = new TasksRepository(db);

if (app.Environment.IsDevelopment())
    app.MapGet("/tasks", () => taskService.GetAll()).RequireAuthorization();

app.MapGet("/tasks/{id}", (string id) => taskService.Get(id)).RequireAuthorization();
app.MapGet("/tasks/all/{id}", (Guid id) => taskService.GetAllForUser(id)).RequireAuthorization();

app.MapPut("/tasks", (CreateTaskModelRequest request) =>
{
    var task = taskService.Add(request, out var id);
    return Results.Created($"/tasks/{id}", task);
}).RequireAuthorization();;

app.MapDelete("/tasks/{id}", (string id, [FromBody] DeleteTaskModelRequest request) => taskService.Delete(id, request.OwnerId)).RequireAuthorization();
app.MapPost("/tasks/{id}", (string id, [FromBody] EditTaskModelRequest request) =>
{
    var success = taskService.Update(id, request);

    return success.Result ? Results.Ok() : Results.BadRequest();
}).RequireAuthorization();

//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.Run();