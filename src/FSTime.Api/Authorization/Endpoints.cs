using FlintSoft.CQRS.Handlers;
using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Authorization.Commands;
using FSTime.Contracts.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSTime.Api.Authorization;

public class Endpoints : IEndpoint
{
    const string COOKIENAME = "refreshToken";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("api/auth");

        grp.MapPost("/login", async (LoginRequest request, HttpResponse response, [FromServices] ICommandHandler<LoginUser.Command, LoginResponse> handler, CancellationToken token) =>
        {
            Guid? tenantId = null;
            if (!string.IsNullOrEmpty(request.TenantId))
            {
                tenantId = Guid.Parse(request.TenantId);
            }

            var result = await handler.Handle(new LoginUser.Command(request.Username, request.Password, tenantId), token);

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

        grp.MapGet("/refresh", async (HttpContext context, [FromServices] ICommandHandler<RefreshToken.Command, RefreshTokenResponse> handler, CancellationToken token) =>
        {
            if (context.Request.Cookies.TryGetValue(COOKIENAME, out var refreshToken))
            {
                var result = await handler.Handle(new RefreshToken.Command(refreshToken), token);
                if (result.IsError)
                {
                    return result.Errors.ToProblemDetails();
                }

                return Results.Ok(result.Value);
            }

            return Results.Unauthorized();
        });

        grp.MapGet("/logout", (HttpRequest request, HttpResponse response) =>
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
