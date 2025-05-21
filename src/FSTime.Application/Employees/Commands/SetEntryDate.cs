using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;

namespace FSTime.Application.Employees.Commands;

public static class SetEntryDate
{
    public record Command(Guid EmployeeId, DateTime Date) : ICommand<EmployeeResponse>;

    internal sealed class Handler(IEmployeeRepository repository) : ICommandHandler<Command, EmployeeResponse>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var employee = await repository.GetEmployee(request.EmployeeId);
            if (employee == null) return EmployeeErrors.Get_Employee_NotFound(request.EmployeeId);

            employee.SetEntryDate(request.Date);

            var result = await repository.UpdateEmployee(employee);
            return result.ToEmployeeResponse();
        }
    }
}