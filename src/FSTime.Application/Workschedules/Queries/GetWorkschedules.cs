using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Application.Workschedules.Queries;

public static class GetWorkschedules
{
    public record Query(Guid companyId) : IQuery<List<WorkSchedule>>;

    internal sealed class Handler(IWorkScheduleRepository repository)
        : IQueryHandler<Query, List<WorkSchedule>>
    {
        public async Task<ErrorOr<List<WorkSchedule>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var workschedules = await repository.GetAll(request.companyId);

            return workschedules;
        }
    }
}