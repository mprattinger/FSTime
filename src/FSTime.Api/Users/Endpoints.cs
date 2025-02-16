using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Users.Commands;
using FSTime.Contracts.Users;
using MediatR;

namespace FSTime.Api.Users;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (RegisterUserRequest request, IMediator mediator) =>
        {
            var command = new CreateUser.Command(request.username, request.password, request.email);

            var result = await mediator.Send(command);

            result.Match(
                id => Results.Created($"users/{id}", id),
                err => err.ToProblemDetails()
                );
        });
    }
}
