using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;

namespace FSTime.Application.Employees.Commands;

public static class SetSupervisor
{
    public record Command(Guid EmployeeId, Guid supervisorId) : IRequest<ErrorOr<EmployeeResponse>>;

    internal sealed class Handler(IEmployeeRepository employeeRepository)
        : IRequestHandler<Command, ErrorOr<EmployeeResponse>>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await employeeRepository.GetEmployee(request.EmployeeId);
                if (employee == null) return EmployeeErrors.Get_Employee_NotFound(request.EmployeeId);

                var supervisor = await employeeRepository.GetEmployee(request.supervisorId);
                if (supervisor == null) return EmployeeErrors.Set_Supervisor_Employee_NotFound(request.supervisorId);

                employee.SetSupervisor(supervisor);

                var result = await employeeRepository.UpdateEmployee(employee);
                return result.ToEmployeeResponse();
            }
            catch (Exception e)
            {
                return EmployeeErrors.SetSupervisor(e.Message);
            }
        }
    }
}