using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Application.Workschedules.Commands;

public static class CreateDailyWorkschedule
{
    public record Command(Guid CompanyId, string Description, Dictionary<DayOfWeek, double> DailyWorktime)
        : IRequest<ErrorOr<WorkSchedule>>;

    internal sealed class Handler(IWorkScheduleRepository repository) : IRequestHandler<Command, ErrorOr<WorkSchedule>>
    {
        public async Task<ErrorOr<WorkSchedule>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var ws = new WorkSchedule(request.CompanyId, request.Description);
                foreach (var (day, worktime) in request.DailyWorktime)
                    switch (day)
                    {
                        case DayOfWeek.Monday:
                            ws.SetMonday(worktime);
                            break;
                        case DayOfWeek.Tuesday:
                            ws.SetTuesday(worktime);
                            break;
                        case DayOfWeek.Wednesday:
                            ws.SetWednesday(worktime);
                            break;
                        case DayOfWeek.Thursday:
                            ws.SetThursday(worktime);
                            break;
                        case DayOfWeek.Friday:
                            ws.SetFriday(worktime);
                            break;
                        case DayOfWeek.Saturday:
                            ws.SetSaturday(worktime);
                            break;
                        case DayOfWeek.Sunday:
                            ws.SetSunday(worktime);
                            break;
                    }

                return await repository.Create(ws);
            }
            catch (Exception e)
            {
                return WorkscheduleErrors.Create_Workschedule(request.Description, e.Message);
            }
        }
    }
}