using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Application;
using Tasks.Api.Domain;

var builder = WebApplication.CreateBuilder(args);
var taskService = new TasksService(builder);
var app = builder.Build();

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