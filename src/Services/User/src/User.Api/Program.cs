var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/user", () => "Hello World!");

app.Run();