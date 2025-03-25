using FSTime.Domain.EmployeeAggregate;
using MediatR;
using ErrorOr;
using FSTime.Application.Common.Interfaces;

namespace FSTime.Application.Employees.Queries;

public static class GetEmployee
{
    public record Query(Guid EmployeeId) : IRequest<ErrorOr<Employee>>;
    
    internal sealed class Handler(IEmployeeRepository employeeRepository) : IRequestHandler<Query, ErrorOr<Employee>>
    {
        public async Task<ErrorOr<Employee>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await employeeRepository.GetEmployee(request.EmployeeId);

                if (employee == null)
                {
                    return EmployeeErrors.Get_Employee_NotFound(request.EmployeeId);
                }
                
                return employee;
            }
            catch (Exception e)
            {
                return EmployeeErrors.Get_Employee(request.EmployeeId, e.Message);
            }
        }
    }
}