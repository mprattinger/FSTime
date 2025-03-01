using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Companies.Commands;
using FSTime.Application.Companies.Queries;
using FSTime.Contracts.Company;
using MediatR;
using OneOf.Types;
using Error = ErrorOr.Error;

namespace FSTime.Api.Companies;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("companies", async (CreateCompanyRequest request, HttpRequest httpRequest, IMediator mediator) =>
        {
            if (!httpRequest.Headers.TryGetValue("TENANT", out var tid))
            {
                return Error.Unexpected("No Tenant provided").ToProblemDetails();
            }

            if (!Guid.TryParse(tid, out var tenantId))
            {
                return Error.Unexpected("No Tenant provided").ToProblemDetails();
            }
            
            var result = await mediator.Send(new CreateCompany.Command(tenantId, request.CompanyName));
            return result.Match(
                id => Results.Created("companies/{id}", id.ToString()),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("TENANT.ADMIN");

        app.MapGet("companies/{tenantId}", async (Guid tenantId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCompaniesByTenant.Query(tenantId));
            return result.Match(
                companies => Results.Ok(companies),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization();
    }
}