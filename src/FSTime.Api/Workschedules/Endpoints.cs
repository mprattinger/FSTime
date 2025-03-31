using FlintSoft.Endpoints;
using FSTime.Api.Common.Errors;
using FSTime.Application.Workplans.Queries;
using FSTime.Application.Workschedules.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSTime.Api.Workschedules;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("workschedules");

        group.MapGet("", async ([FromQuery] Guid company, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetWorkschedules.Query(company));

            return result.Match(
                plans => Results.Ok(plans),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        });

        group.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetWorkschedule.Query(id));

            return result.Match(
                plan => Results.Ok(plan),
                error => Results.BadRequest(error.ToProblemDetails())
            );
        });
    }
}