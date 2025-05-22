using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;

namespace FSTime.Application.Employees.Commands;

public static class SetHead
{
    public record Command(Guid EmployeeId) : ICommand<EmployeeResponse>;

    internal sealed class Handler(IEmployeeRepository employeeRepository)
        : ICommandHandler<Command, EmployeeResponse>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetEmployee(request.EmployeeId);
            if (employee == null) return EmployeeErrors.Get_Employee_NotFound(request.EmployeeId);

            var currentHead = await employeeRepository.GetHead();
            if (currentHead is not null)
            {
                if (currentHead.Id != request.EmployeeId) return currentHead.ToEmployeeResponse();
                return EmployeeErrors.HeadAlreadyExists(currentHead.Id);
            }

            employee.SetIsHead();

            var result = await employeeRepository.UpdateEmployee(employee);
            return result.ToEmployeeResponse();
        }
    }
}