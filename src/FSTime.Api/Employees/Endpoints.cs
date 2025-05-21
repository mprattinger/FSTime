using ErrorOr;
using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Common;
using FSTime.Application.Employees.Commands;
using FSTime.Application.Employees.Queries;
using FSTime.Contracts.Employees;
using Microsoft.AspNetCore.Mvc;

namespace FSTime.Api.Employees;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("api/employees");

        grp.MapGet("", async ([FromQuery] Guid company, [FromServices] IQeryHandler<GetAllEmployees.Query, List<EmployeeResponse>> handler) =>
        {
            var result = await handler.Handle(new GetAllEmployees.Query(company));

            return result.Match(
                emps => Results.Ok(emps),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("EMPLOYEE.Read");

        grp.MapGet("/{id}", async (Guid id, [FromServices] IQeryHandler<GetEmployee.Query, EmployeeResponse> handler) =>
        {
            var result = await handler.Handle(new GetEmployee.Query(id));

            return result.Match(
                emp => Results.Ok(emp),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("EMPLOYEE.Read_SELF, EMPLOYEE.Read");

        grp.MapGet("/me", async (HttpContext context, [FromServices] IQeryHandler<GetEmployeeByUserId.Query, EmployeeResponse> handler) =>
        {
            //Userid ermitteln
            var userId = context.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return Results.Unauthorized();
            }

            var result = await handler.Handle(new GetEmployeeByUserId.Query(parsedUserId));

            return result.Match(
                emp => Results.Ok(emp),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("EMPLOYEE.Read_SELF");

        grp.MapPost("", async (CreateEmployeeRequest request, [FromQuery] Guid company, [FromServices] ICommandHandler<CreateEmployee.Command, EmployeeResponse> handler) =>
        {
            var result = await handler.Handle(new CreateEmployee.Command(company, request.FirstName, request.LastName,
                request.MiddleName));

            return result.Match(
                emp => Results.Created($"companies/{emp.Id}", emp),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("EMPLOYEE.Update");

        grp.MapPost("/assignUser", async (AssignUserRequest request, HttpContext context, [FromServices] ICommandHandler<AssignUserToEmployee.Command, EmployeeResponse> handler) =>
        {
            var tenantId = context.GetTenantIdFromHttpContext();
            if (tenantId is null) return Results.Unauthorized();

            var result =
                await handler.Handle(
                    new AssignUserToEmployee.Command(request.EmployeeId, request.UserId, (Guid)tenantId));
            return result.Match(
                emp => Results.Ok(emp),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("EMPLOYEE.Update");

        grp.MapPost("/addworkschedule", async (AddWorkscheduleRequest request, [FromServices] ICommandHandler<AddWorkschedule.Command, EmployeeResponse> handler) =>
        {
            var result = await handler.Handle(new AddWorkschedule.Command(request.EmployeeId, request.WorkscheduleId,
                request.ValidFrom.ToDateTime(TimeOnly.MinValue).ToUniversalTime()));

            return result.Match(
                emp => Results.Ok(emp),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("EMPLOYEE.Update");
    }
}