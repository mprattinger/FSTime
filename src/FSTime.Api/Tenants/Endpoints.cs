using FlintSoft.CQRS.Handlers;
using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Common;
using FSTime.Application.Tenants.Commands;
using FSTime.Application.Tenants.Queries;
using FSTime.Contracts.Tenants;
using FSTime.Domain.TenantAggregate;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FSTime.Api.Tenants;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("api/tenants");

        grp.MapPost("", async (CreateTenantRequest request, [FromServices] ICommandHandler<CreateTenant.Command, Guid> handler, HttpContext context, CancellationToken token) =>
        {
            if (context.User.Identity is null) return Results.Unauthorized();
            if (!context.User.Identity.IsAuthenticated) return Results.Unauthorized();

            var idClaim = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (idClaim is null) return Results.Unauthorized();

            var guid = Guid.Parse(idClaim.Value);
            var result = await handler.Handle(new CreateTenant.Command(request.Name, guid), token);

            return result.Match(
                id => Results.Created($"tenants/{id}", id),
                err => err.ToProblemDetails()
            );
        }).RequireAuthorization();

        grp.MapGet("", async (HttpContext context, [FromServices] IQueryHandler<GetTenantById.Query, Tenant> handler, CancellationToken token) =>
        {
            if (context.User.Identity is null) return Results.Unauthorized();
            if (!context.User.Identity.IsAuthenticated) return Results.Unauthorized();

            var idClaim = context.GetTenantIdFromHttpContext();
            if (idClaim is null) return Results.Unauthorized();

            var result = await handler.Handle(new GetTenantById.Query(idClaim.Value), token);
            return result.Match(
                t => Results.Ok(t),
                err => err.ToProblemDetails()
            );
        }).RequireAuthorization();

        grp.MapPost("/adduser", async (AssignUserRequest request, [FromServices] ICommandHandler<AddUserToTenant.Command, Tenant> handler, HttpContext context, CancellationToken token) =>
        {
            if (context.User.Identity is null) return Results.Unauthorized();
            if (!context.User.Identity.IsAuthenticated) return Results.Unauthorized();

            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            var userId = Guid.Parse(request.UserId);

            var result = await handler.Handle(new AddUserToTenant.Command((Guid)tenantId, userId, request.Role), token);
            return result.Match(
                t => Results.Ok(t),
                err => err.ToProblemDetails()
            );
        }).RequireAuthorization("TENANT.ADMIN");
    }
}