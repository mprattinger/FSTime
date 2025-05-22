using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Application.Workschedules;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Application.Workplans.Queries;

public static class GetWorkschedule
{
    public record Query(Guid id) : IQuery<WorkSchedule>;

    internal sealed class Handler(IWorkScheduleRepository repository) : IQueryHandler<Query, WorkSchedule>
    {
        public async Task<ErrorOr<WorkSchedule>> Handle(Query request, CancellationToken cancellationToken)
        {
            var workschedule = await repository.Get(request.id);

            if (workschedule is null) return WorkscheduleErrors.Workschedule_NotFound(request.id);

            return workschedule;
        }
    }
}