using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;

namespace FSTime.Application.Employees.Commands;

public static class SetSupervisor
{
    public record Command(Guid EmployeeId, Guid supervisorId) : ICommand<EmployeeResponse>;

    internal sealed class Handler(IEmployeeRepository employeeRepository)
        : ICommandHandler<Command, EmployeeResponse>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetEmployee(request.EmployeeId);
            if (employee == null) return EmployeeErrors.Get_Employee_NotFound(request.EmployeeId);

            var supervisor = await employeeRepository.GetEmployee(request.supervisorId);
            if (supervisor == null) return EmployeeErrors.Set_Supervisor_Employee_NotFound(request.supervisorId);

            employee.SetSupervisor(supervisor);

            var result = await employeeRepository.UpdateEmployee(employee);
            return result.ToEmployeeResponse();
        }
    }
}