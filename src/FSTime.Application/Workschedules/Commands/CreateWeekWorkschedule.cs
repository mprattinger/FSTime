using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.WorkScheduleAggregate;
using MediatR;

namespace FSTime.Application.Workschedules.Commands;

public static class CreateWeekWorkschedule
{
    public record Command(Guid CompanyId, string Description, double WeekWorktime, int WeekWorkDays)
        : IRequest<ErrorOr<WorkSchedule>>;

    internal sealed class Handler(IWorkScheduleRepository repository) : IRequestHandler<Command, ErrorOr<WorkSchedule>>
    {
        public async Task<ErrorOr<WorkSchedule>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var ws = new WorkSchedule(request.CompanyId, request.Description, request.WeekWorktime,
                    request.WeekWorkDays);
                return await repository.Create(ws);
            }
            catch (Exception e)
            {
                return WorkscheduleErrors.Create_Workschedule(request.Description, e.Message);
            }
        }
    }
}