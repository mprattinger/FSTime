using FSTime.Domain.EmployeeAggregate;
using MediatR;
using ErrorOr;
using FSTime.Application.Common.Interfaces;

namespace FSTime.Application.Employees.Queries;

public static class GetAllEmployees
{
    public record Query(Guid companyId) : IRequest<ErrorOr<List<Employee>>>;
    
    internal sealed class Handler(IEmployeeRepository employeeRepository) : IRequestHandler<Query, ErrorOr<List<Employee>>>
    {
        public async Task<ErrorOr<List<Employee>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await employeeRepository.GetEmployees(request.companyId);

                return employees;
            }
            catch (Exception e)
            {
               return EmployeeErrors.Get_All_Employees(e.Message);
            }
        }
    }
}