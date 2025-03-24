using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Employees.Queries;
using FSTime.Domain.CompanyAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSTime.Api.Employees;

public class Endpoints: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("employees");

        grp.MapGet("", async ([FromQuery]Guid company, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllEmployees.Query(company));

            return result.Match(
                emps => Results.Ok(emps),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        });
        
        grp.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetEmployee.Query(id));

            return result.Match(
                emps => Results.Ok(emps),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        });
    }
}