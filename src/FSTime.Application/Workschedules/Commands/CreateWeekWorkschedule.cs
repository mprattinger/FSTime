using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Application.Workschedules.Commands;

public static class CreateWeekWorkschedule
{
    public record Command(Guid CompanyId, string Description, double WeekWorktime, int WeekWorkDays)
        : ICommand<WorkSchedule>;

    internal sealed class Handler(IWorkScheduleRepository repository) : ICommandHandler<Command, WorkSchedule>
    {
        public async Task<ErrorOr<WorkSchedule>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ws = new WorkSchedule(request.CompanyId, request.Description, request.WeekWorktime,
                request.WeekWorkDays);
            return await repository.Create(ws);
        }
    }
}