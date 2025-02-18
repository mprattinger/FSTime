using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application;
using FSTime.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.MapEndpoints();

app.UseExceptionHandler();

app.Run();
