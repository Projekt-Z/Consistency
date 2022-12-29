using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Application;
using Tasks.Api.Domain;

var builder = WebApplication.CreateBuilder(args);
var taskService = new TasksService();
var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.MapGet("/", () => taskService.GetAll());

app.MapGet("/{id}", (Guid id) => taskService.Get(id));
app.MapPut("/", (CreateTaskModelRequest request) =>
{
    var task = taskService.Add(request, out var id);
    return Results.Created($"/{id}", task);
});

app.MapDelete("/{id}", (Guid id, [FromBody] DeleteTaskModelRequest request) => taskService.Delete(id, request.OwnerId));
app.MapPost("/{id}", (Guid id, [FromBody] CreateTaskModelRequest request) =>
{
    var success = taskService.Update(id, request);

    return success ? Results.Ok() : Results.BadRequest();
});

app.Run();