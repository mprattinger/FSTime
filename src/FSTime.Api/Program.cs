using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application;
using FSTime.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.AddInfrastructure();
builder.AddApplication();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.MapEndpoints();

app.UseExceptionHandler();

app.Run();
