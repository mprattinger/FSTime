using ErrorOr;
using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Common;
using FSTime.Application.Companies.Commands;
using FSTime.Application.Companies.Queries;
using FSTime.Contracts.Company;
using FSTime.Domain.CompanyAggregate;
using Microsoft.AspNetCore.Mvc;

namespace FSTime.Api.Companies;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("api/companies");

        grp.MapPost("", async (CreateCompanyRequest request, HttpContext context, [FromServices] ICommandHandler<CreateCompany.Command, Guid> handler) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            var result = await handler.Handle(new CreateCompany.Command(tenantId.Value, request.CompanyName));
            return result.Match(
                id => Results.Created("companies/{id}", id.ToString()),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("TENANT.ADMIN");

        grp.MapGet("", async (HttpContext context, [FromServices] IQueryHandler<GetCompaniesByTenant.Query, List<Company>> handler) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            var result = await handler.Handle(new GetCompaniesByTenant.Query(tenantId.Value));
            return result.Match(
                companies => Results.Ok(companies),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization();
    }
}