using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Tasks.Application;
using Tasks.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

var connectionString = app.Configuration.GetConnectionString("mongodb");
//var mongoUrl = MongoUrl.Create(connectionString);
var mongoClient = new MongoClient(connectionString);
var db = mongoClient.GetDatabase("ctaskdb");

var taskService = new TasksRepository(db);

if(app.Environment.IsDevelopment())
    app.MapGet("/tasks", () => taskService.GetAll());

app.MapGet("/tasks/{id}", (string id) => taskService.Get(id));
app.MapGet("/tasks/all/{id}", (Guid id) => taskService.GetAllForUser(id));

app.MapPut("/tasks", (CreateTaskModelRequest request) =>
{
    var task = taskService.Add(request, out var id);
    return Results.Created($"/tasks/{id}", task);
});

app.MapDelete("/tasks/{id}", (string id, [FromBody] DeleteTaskModelRequest request) => taskService.Delete(id, request.OwnerId));
app.MapPost("/tasks/{id}", (string id, [FromBody] CreateTaskModelRequest request) =>
{
    var success = taskService.Update(id, request);

    return success.Result ? Results.Ok() : Results.BadRequest();
});

app.UseCors("CorsPolicy");

app.Run();