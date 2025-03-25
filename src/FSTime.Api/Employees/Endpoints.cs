using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Employees.Commands;
using FSTime.Application.Employees.Queries;
using FSTime.Contracts.Employees;
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
        }).RequireAuthorization("EMPLOYEE.READ");
        
        grp.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetEmployee.Query(id));

            return result.Match(
                emp => Results.Ok(emp),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("EMPLOYEE.READ, EMPLOYEE.READ_SELF");

        grp.MapPost("", async (CreateEmployeeRequest request, [FromQuery]Guid company, IMediator mediator) =>
        {
            var result = await mediator.Send(new CreateEmployee.Command(company, request.FirstName, request.LastName, request.MiddleName));
            
            return result.Match(
                emp => Results.Created($"companies/{emp.Id}", emp),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("EMPLOYEE.MODIFY");
    }
}