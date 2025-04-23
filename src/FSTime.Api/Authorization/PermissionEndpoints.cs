using ErrorOr;
using FlintSoft.CQRS;
using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Authorization.Commands;
using FSTime.Application.Authorization.Queries;
using FSTime.Application.Common;
using FSTime.Contracts.Authorization;
using FSTime.Domain.AuthorizationAggregate;
using Microsoft.AspNetCore.Mvc;

namespace FSTime.Api.Authorization;

public class PermissionEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("permissions");

        grp.MapGet("/my", async (HttpContext context, [FromServices] IRequestHandler<GetPermissionsForUser.Query, ErrorOr<List<Permission>>> handler) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            var userId = context.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            if (!Guid.TryParse(userId, out var parsedUserId)) return Results.Unauthorized();

            var result = await handler.Handle(new GetPermissionsForUser.Query(tenantId.Value, parsedUserId));

            return result.Match(
                permissions => Results.Ok(permissions),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization();

        grp.MapGet("/{userId}", async (Guid userId, HttpContext context, [FromServices] IRequestHandler<GetPermissionsForUser.Query, ErrorOr<List<Permission>>> handler) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            var result = await handler.Handle(new GetPermissionsForUser.Query(tenantId.Value, userId));

            return result.Match(
                permissions => Results.Ok(permissions),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("TENANT.ADMIN");

        grp.MapGet("/groups", async ([FromServices] IRequestHandler<GetGroups.Query, ErrorOr<List<string>>> handler) =>
        {
            var result = await handler.Handle(new GetGroups.Query());

            return result.Match(
                groups => Results.Ok(groups),
                error => Results.BadRequest(error.ToProblemDetails()));
        }).RequireAuthorization("TENANT.ADMIN");

        grp.MapGet("/actions/{group}", async (string group, [FromServices] IRequestHandler<GetActions.Query, ErrorOr<List<string>>> handler) =>
        {
            var result = await handler.Handle(new GetActions.Query(group));

            return result.Match(
                groups => Results.Ok(groups),
                error => Results.BadRequest(error.ToProblemDetails()));
        }).RequireAuthorization("TENANT.ADMIN");

        grp.MapPost("/", async (SetPermissionRequest data, HttpContext context, [FromServices] IRequestHandler<SetPermission.Command, ErrorOr<List<Permission>>> handler) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            if (!Enum.TryParse<PermissionAction>(data.Action, out var action))
            {
                return Results.BadRequest(new ProblemDetails
                {
                    Title = "Invalid Action",
                    Detail = $"The action '{data.Action}' is not valid."
                });
            }

            var result = await handler.Handle(new SetPermission.Command(tenantId.Value, data.UserId, data.Group, action));

            return result.Match(
                perm => Results.Ok(perm),
                error => Results.BadRequest(error.ToProblemDetails()));
        }).RequireAuthorization("TENANT.ADMIN");
    }
}