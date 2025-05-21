using ErrorOr;
using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Workplans.Queries;
using FSTime.Application.Workschedules.Commands;
using FSTime.Application.Workschedules.Queries;
using FSTime.Contracts.WorkSchedule;
using FSTime.Domain.WorkScheduleAggregate;
using Microsoft.AspNetCore.Mvc;

namespace FSTime.Api.Workschedules;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/workschedules");

        group.MapGet("", async ([FromQuery] Guid company, [FromServices] IQueryHandler<GetWorkschedules.Query, List<WorkSchedule>> handler) =>
        {
            var result = await handler.Handle(new GetWorkschedules.Query(company));

            return result.Match(
                plans => Results.Ok(plans),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        });

        group.MapGet("/{id}", async (Guid id, [FromServices] IQueryHandler<GetWorkschedule.Query, WorkSchedule> handler) =>
        {
            var result = await handler.Handle(new GetWorkschedule.Query(id));

            return result.Match(
                plan => Results.Ok(plan),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        }).RequireAuthorization("WORKSCHEDULE.Read");

        group.MapPost("/daily",
            async ([FromQuery] Guid company, DailyWorkscheduleRequest request, [FromServices] ICommandHandler<CreateDailyWorkschedule.Command, WorkSchedule> handler) =>
            {
                var days = new Dictionary<DayOfWeek, double>();
                days.Add(DayOfWeek.Monday, request.Monday);
                days.Add(DayOfWeek.Tuesday, request.Tuesday);
                days.Add(DayOfWeek.Wednesday, request.Wednesday);
                days.Add(DayOfWeek.Thursday, request.Thursday);
                days.Add(DayOfWeek.Friday, request.Friday);
                days.Add(DayOfWeek.Saturday, request.Saturday);
                days.Add(DayOfWeek.Sunday, request.Sunday);

                var result =
                    await handler.Handle(new CreateDailyWorkschedule.Command(company, request.Description, days));

                return result.Match(
                    plan => Results.Ok(plan),
                    error => Results.BadRequest(error.ToProblemDetails())
                );
            }).RequireAuthorization("WORKSCHEDULE.Update");

        group.MapPost("/weekly",
            async ([FromQuery] Guid company, WeeklyWorkscheduleRequest request, [FromServices] ICommandHandler<CreateWeekWorkschedule.Command, WorkSchedule> handler) =>
            {
                var result =
                    await handler.Handle(new CreateWeekWorkschedule.Command(company, request.Description,
                        request.WeeklyWorktime, request.Workdays));

                return result.Match(
                    plan => Results.Ok(plan),
                    error => Results.BadRequest(error.ToProblemDetails())
                );
            }).RequireAuthorization("WORKSCHEDULE.Update");
    }
}