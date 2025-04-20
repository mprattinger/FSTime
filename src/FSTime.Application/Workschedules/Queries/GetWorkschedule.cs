using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Application.Workschedules;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Application.Workplans.Queries;

public static class GetWorkschedule
{
    public record Query(Guid id) : IRequest<ErrorOr<WorkSchedule>>;

    internal sealed class Handler(IWorkScheduleRepository repository) : IRequestHandler<Query, ErrorOr<WorkSchedule>>
    {
        public async Task<ErrorOr<WorkSchedule>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var workschedule = await repository.Get(request.id);

                if (workschedule is null) return WorkscheduleErrors.Workschedule_NotFound(request.id);

                return workschedule;
            }
            catch (Exception e)
            {
                return WorkscheduleErrors.Get_Workschedule(request.id, e.Message);
            }
        }
    }
}