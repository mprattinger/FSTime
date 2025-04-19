using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;
using MediatR;

namespace FSTime.Application.Employees.Commands;

public static class AddWorkschedule
{
    public record Command(Guid EmployeeId, Guid WorkscheduleId, DateTime ValidFrom)
        : IRequest<ErrorOr<EmployeeResponse>>;

    internal sealed class Handler(IEmployeeRepository employeeRepository)
        : IRequestHandler<Command, ErrorOr<EmployeeResponse>>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await employeeRepository.AddWorkschedule(request.EmployeeId, request.WorkscheduleId,
                    request.ValidFrom);

                return employee.ToEmployeeResponse();
            }
            catch (Exception e)
            {
                return EmployeeErrors.AddWorkschedule(e.Message);
            }
        }
    }
}