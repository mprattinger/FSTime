using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Authorization.Queries;
using FSTime.Application.Common;
using MediatR;

namespace FSTime.Api.Authorization;

public class PermissionEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("permissions");

        grp.MapGet("/my", async (HttpContext context, IMediator mediator) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            var userId = context.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            if (!Guid.TryParse(userId, out var parsedUserId)) return Results.Unauthorized();

            var result = await mediator.Send(new GetPermissionsForUser.Query(tenantId.Value, parsedUserId));

            return result.Match(
                permissions => Results.Ok(permissions),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization();

        grp.MapGet("/{userId}", async (Guid userId, HttpContext context, IMediator mediator) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            var result = await mediator.Send(new GetPermissionsForUser.Query(tenantId.Value, userId));

            return result.Match(
                permissions => Results.Ok(permissions),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("TENANT.ADMIN");
    }
}