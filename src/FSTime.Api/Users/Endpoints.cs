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
        var grp = app.MapGroup("users");
        
        grp.MapPost("", async (RegisterUserRequest request, IMediator mediator, HttpContext context) =>
        {
            var command = new CreateUser.Command(request.username, request.password, request.email);

            var result = await mediator.Send(command);
            
            var url = $"{context.Request.Scheme}://{context.Request.Host}";
            url = $"{url}/users/verify?token={result.Value.VerifyToken}&email={result.Value.Email}";
            Console.WriteLine(url);
            
            result.Match(
                id => Results.Created($"users/{id}", id.UserId),
                err => err.ToProblemDetails()
                );
        });

        grp.MapGet("/verify", async (string token, string email, IMediator mediator) =>
        {
            var result = await mediator.Send(new VerifyUser.Command(token, email));
            
            result.Match(
                _ => Results.Ok(),
                err => err.ToProblemDetails()
                );
        });
    }
}
