using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;
using MediatR;

namespace FSTime.Application.Employees.Commands;

public static class AssignUserToEmployee
{
    public record Command(Guid EmployeeId, Guid UserId, Guid TenantId) : IRequest<ErrorOr<EmployeeResponse>>;

    internal sealed class Handler(
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository,
        ITenantRepository tenantRepository)
        : IRequestHandler<Command, ErrorOr<EmployeeResponse>>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                //User must be in the current tenant
                if (!await tenantRepository.IsTenantUser(request.TenantId, request.UserId))
                    return EmployeeErrors.EmployeeNotInTenant(request.UserId);

                var employee = await employeeRepository.GetEmployee(request.EmployeeId);
                if (employee == null) return EmployeeErrors.Get_Employee_NotFound(request.EmployeeId);

                var user = await userRepository.GetUserById(request.UserId);
                if (user is null) return EmployeeErrors.Get_Employee_User_NotFound(request.UserId);

                employee.AssignUser(user);

                var result = await employeeRepository.UpdateEmployee(employee);
                return result.ToEmployeeResponse();
            }
            catch (Exception e)
            {
                return EmployeeErrors.AssignUser(e.Message);
            }
        }
    }
}