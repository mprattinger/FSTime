using ErrorOr;
using FlintSoft.CQRS.Handlers;
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
        var grp = app.MapGroup("api/users");

        grp.MapPost("", async ([FromBody] RegisterUserRequest request, HttpContext context, [FromServices] ICommandHandler<CreateUser.Command, RegisterUserResult> handler, CancellationToken token) =>
        {
            var command = new CreateUser.Command(request.username, request.password, request.email);

            var result = await handler.Handle(command, token);

            var url = $"{context.Request.Scheme}://{context.Request.Host}";
            url = $"{url}/users/verify?token={result.Value.VerifyToken}&email={result.Value.Email}";
            Console.WriteLine(url);

            return result.Match(
                id => Results.Created($"users/{id}", id.UserId),
                err => err.ToProblemDetails()
                );
        });

        grp.MapGet("/verify", async (string token, string email, [FromServices] ICommandHandler<VerifyUser.Command, Success> handler, CancellationToken ctoken) =>
        {
            var result = await handler.Handle(new VerifyUser.Command(token, email), ctoken);

            return result.Match(
                _ => Results.Ok(),
                err => err.ToProblemDetails()
                );
        });
    }
}
