using ErrorOr;
using FlintSoft.CQRS;
using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Users.Commands;
using FSTime.Contracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace FSTime.Api.Users;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("users");

        grp.MapPost("", async ([FromBody] RegisterUserRequest request, HttpContext context, [FromServices] IRequestHandler<CreateUser.Command, ErrorOr<RegisterUserResult>> handler) =>
        {
            var command = new CreateUser.Command(request.username, request.password, request.email);

            var result = await handler.Handle(command);

            var url = $"{context.Request.Scheme}://{context.Request.Host}";
            url = $"{url}/users/verify?token={result.Value.VerifyToken}&email={result.Value.Email}";
            Console.WriteLine(url);

            return result.Match(
                id => Results.Created($"users/{id}", id.UserId),
                err => err.ToProblemDetails()
                );
        });

        grp.MapGet("/verify", async (string token, string email, [FromServices] IRequestHandler<VerifyUser.Command, ErrorOr<Success>> handler) =>
        {
            var result = await handler.Handle(new VerifyUser.Command(token, email));

            return result.Match(
                _ => Results.Ok(),
                err => err.ToProblemDetails()
                );
        });
    }
}
