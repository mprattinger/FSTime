using FlintSoft.Endpoints;
using FSTime.Api;
using FSTime.Api.Common.Errors;
using FSTime.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.UseExceptionHandler();

app.Run();
