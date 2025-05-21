using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;

namespace FSTime.Application.Employees.Commands;

public static class AddWorkschedule
{
    public record Command(Guid EmployeeId, Guid WorkscheduleId, DateTime ValidFrom)
        : ICommand<EmployeeResponse>;

    internal sealed class Handler(IEmployeeRepository employeeRepository)
        : ICommandHandler<Command, EmployeeResponse>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.AddWorkschedule(request.EmployeeId, request.WorkscheduleId,
                request.ValidFrom);

            return employee.ToEmployeeResponse();
        }
    }
}