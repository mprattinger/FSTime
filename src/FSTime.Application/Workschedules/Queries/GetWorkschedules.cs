using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.WorkScheduleAggregate;
using MediatR;

namespace FSTime.Application.Workschedules.Queries;

public static class GetWorkschedules
{
    public record Query(Guid companyId) : IRequest<ErrorOr<List<WorkSchedule>>>;

    internal sealed class Handler(IWorkScheduleRepository repository)
        : IRequestHandler<Query, ErrorOr<List<WorkSchedule>>>
    {
        public async Task<ErrorOr<List<WorkSchedule>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var workschedules = await repository.GetAll(request.companyId);

                return workschedules;
            }
            catch (Exception e)
            {
                return WorkscheduleErrors.Get_Workschedules(e.Message);
            }
        }
    }
}