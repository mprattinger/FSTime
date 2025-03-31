using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;
using FSTime.Contracts.Users;
using MediatR;

namespace FSTime.Application.Employees.Queries;

public static class GetEmployee
{
    public record Query(Guid EmployeeId) : IRequest<ErrorOr<EmployeeResponse>>;

    internal sealed class Handler(IEmployeeRepository employeeRepository)
        : IRequestHandler<Query, ErrorOr<EmployeeResponse>>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await employeeRepository.GetEmployee(request.EmployeeId);

                if (employee == null) return EmployeeErrors.Get_Employee_NotFound(request.EmployeeId);

                var employeeResponse = new EmployeeResponse
                {
                    Id = employee.Id,
                    CompanyId = employee.CompanyId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    MiddleName = employee.MiddleName,
                    EmployeeCode = employee.EmployeeCode,
                    EntryDate = employee.EntryDate,
                    WorkplanId = employee.WorkplanId,
                    User = employee.User != null
                        ? new UserResponse
                        {
                            Id = employee.User.Id,
                            UserName = employee.User.UserName,
                            Email = employee.User.Email,
                            Verified = employee.User.Verified
                        }
                        : null,
                    Supervisor = employee.Supervisor != null
                        ? new EmployeeResponse
                        {
                            Id = employee.Supervisor.Id,
                            CompanyId = employee.Supervisor.CompanyId,
                            FirstName = employee.Supervisor.FirstName,
                            LastName = employee.Supervisor.LastName,
                            MiddleName = employee.Supervisor.MiddleName,
                            EmployeeCode = employee.Supervisor.EmployeeCode,
                            EntryDate = employee.Supervisor.EntryDate,
                            WorkplanId = employee.Supervisor.WorkplanId,
                            IsHead = employee.Supervisor.IsHead
                        }
                        : null,
                    IsHead = employee.IsHead
                };

                return employeeResponse;
            }
            catch (Exception e)
            {
                return EmployeeErrors.Get_Employee(request.EmployeeId, e.Message);
            }
        }
    }
}