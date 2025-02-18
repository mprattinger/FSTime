using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Authorization.Commands;
using FSTime.Contracts.Authorization;
using MediatR;

namespace FSTime.Api.Authorization;

public class Endpoints : IEndpoint
{
    const string COOKIENAME = "refresh_token";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login", async (LoginRequest request, IMediator mediator, HttpResponse response) =>
        {
            var result = await mediator.Send(new LoginUser.Command(request.Username, request.Password));

            if (result.IsError)
            {
                return result.Errors.ToProblemDetails();
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = result.Value.RefreshTokenExpires
            };
            response.Cookies.Append(COOKIENAME, result.Value.RefereshToken, cookieOptions);

            return Results.Ok(result.Value);
        });

        app.MapGet("auth/refresh", async (HttpContext context, IMediator mediator) =>
        {
            if (context.Request.Cookies.TryGetValue(COOKIENAME, out var refreshToken))
            {
                var result = await mediator.Send(new RefreshToken.Command(refreshToken));
                if (result.IsError)
                {
                    return result.Errors.ToProblemDetails();
                }

                return Results.Ok(result.Value);
            }

            return Results.Unauthorized();
        });

        app.MapGet("auth/logout", async (HttpRequest request, HttpResponse response) =>
        {
            if (!request.Cookies.Any(x => x.Key == COOKIENAME))
            {
                return Results.NoContent();
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
            };

            response.Cookies.Delete(COOKIENAME, cookieOptions);

            return Results.NoContent();
        });
    }
}
