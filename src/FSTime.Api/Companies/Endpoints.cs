using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Companies.Commands;
using FSTime.Application.Companies.Queries;
using FSTime.Contracts.Authorization;
using FSTime.Contracts.Company;
using MediatR;

namespace FSTime.Api.Companies;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("companies");
        
        grp.MapPost("", async (CreateCompanyRequest request, HttpContext context, IMediator mediator) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null)
            {
                return Results.Unauthorized();
            }

            var result = await mediator.Send(new CreateCompany.Command(tenantId.Value, request.CompanyName));
            return result.Match(
                id => Results.Created("companies/{id}", id.ToString()),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("TENANT.ADMIN");

        grp.MapGet("", async (HttpContext context, IMediator mediator) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null)
            {
                return Results.Unauthorized();
            }

            var result = await mediator.Send(new GetCompaniesByTenant.Query(tenantId.Value));
            return result.Match(
                companies => Results.Ok(companies),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization();
    }
}