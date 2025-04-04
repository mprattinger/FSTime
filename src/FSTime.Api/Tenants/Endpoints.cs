using System.IdentityModel.Tokens.Jwt;
using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Api.Companies;
using FSTime.Application.Tenants.Commands;
using FSTime.Application.Tenants.Queries;
using FSTime.Contracts.Authorization;
using FSTime.Contracts.Tenants;
using FSTime.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FSTime.Api.Tenants;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("tenants");
        
        grp.MapPost("", async (CreateTenantRequest request, IMediator mediator, HttpContext context) =>
        {
            if (context.User.Identity is null)
            {
                return Results.Unauthorized();
            }
            if (!context.User.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
            }
            
            var idClaim = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if(idClaim is null)
            {
                return Results.Unauthorized();
            }

            var guid = Guid.Parse(idClaim.Value);
            var result = await mediator.Send(new CreateTenant.Command(request.Name, guid));
            
            return result.Match(
                id => Results.Created($"tenants/{id}", id),
                err => err.ToProblemDetails()
                );
        }).RequireAuthorization();

        grp.MapGet("", async (HttpContext context, IMediator mediator) =>
        {
            if (context.User.Identity is null)
            {
                return Results.Unauthorized();
            }
            if (!context.User.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
            }

            var idClaim = context.GetTenantIdFromHttpContext();
            if(idClaim is null)
            {
                return Results.Unauthorized();
            }

            var result = await mediator.Send(new GetTenantById.Query(idClaim.Value));
            return result.Match(
                t => Results.Ok(t),
                err => err.ToProblemDetails()
            );
        }).RequireAuthorization();

        grp.MapPost("/adduser", async (AssignUserRequest request, IMediator mediator, HttpContext context) =>
        {
            if (context.User.Identity is null)
            {
                return Results.Unauthorized();
            }
            if (!context.User.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
            }

            var tenantId = context.GetTenantIdFromHttpContext();
            if(tenantId is null)
            {
                return Results.Unauthorized();
            }
            
            var userId = Guid.Parse(request.UserId);
            
            var result = await mediator.Send(new AddUserToTenant.Command((Guid)tenantId, userId, request.Role));
            return result.Match(
                t => Results.Ok(t),
                err => err.ToProblemDetails()
                );
        }).RequireAuthorization("TENANT.ADMIN");
    }
}