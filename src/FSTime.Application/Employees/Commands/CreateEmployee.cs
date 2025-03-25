using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.EmployeeAggregate;
using MediatR;

namespace FSTime.Application.Employees.Commands;

public static class CreateEmployee
{
    public record Command(Guid CompanyId, string FirstName, string LastName, string? MiddleName) : IRequest<ErrorOr<Employee>>;
    
    internal sealed class Handler(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository) : IRequestHandler<Command, ErrorOr<Employee>>
    {
        public async Task<ErrorOr<Employee>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await companyRepository.GetCompanyById(request.CompanyId);
                if (company is null)
                {
                    return EmployeeErrors.CreateEmployee_NoCompany(request.CompanyId);
                }
                
                var employee = new Employee(request.CompanyId, request.FirstName, request.LastName, request.MiddleName);
                var result = await employeeRepository.CreateEmployee(employee);
                return result;
            }
            catch (Exception e)
            {
                return EmployeeErrors.CreateEmployee(e.Message);
            }
        }
    }
}