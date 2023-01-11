using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Tasks.Application;
using Tasks.Domain;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var connectionString = app.Configuration.GetConnectionString("mongodb");
//var mongoUrl = MongoUrl.Create(connectionString);
var mongoClient = new MongoClient(connectionString);
var db = mongoClient.GetDatabase("ctaskdb");

var taskService = new TasksRepository(db);

if(app.Environment.IsDevelopment())
    app.MapGet("/", () => taskService.GetAll());

app.MapGet("/{id}", (string id) => taskService.Get(id));
app.MapGet("/all/{id}", (Guid id) => taskService.GetAllForUser(id));

app.MapPut("/", (CreateTaskModelRequest request) =>
{
    var task = taskService.Add(request, out var id);
    return Results.Created($"/{id}", task);
});

app.MapDelete("/{id}", (string id, [FromBody] DeleteTaskModelRequest request) => taskService.Delete(id, request.OwnerId));
app.MapPost("/{id}", (string id, [FromBody] CreateTaskModelRequest request) =>
{
    var success = taskService.Update(id, request);

    return success.Result ? Results.Ok() : Results.BadRequest();
});

app.Run();