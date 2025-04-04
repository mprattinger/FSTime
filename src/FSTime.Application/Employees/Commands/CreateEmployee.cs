using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;
using FSTime.Domain.EmployeeAggregate;
using MediatR;

namespace FSTime.Application.Employees.Commands;

public static class CreateEmployee
{
    public record Command(
        Guid CompanyId,
        string FirstName,
        string LastName,
        string? MiddleName = "",
        DateTime? entryDate = null,
        Guid? supervisorId = null,
        bool? isHead = null)
        : IRequest<ErrorOr<EmployeeResponse>>;

    internal sealed class Handler(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
        : IRequestHandler<Command, ErrorOr<EmployeeResponse>>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await companyRepository.GetCompanyById(request.CompanyId);
                if (company is null) return EmployeeErrors.CreateEmployee_NoCompany(request.CompanyId);

                var employee = new Employee(request.CompanyId, request.FirstName, request.LastName, request.MiddleName,
                    request.entryDate, request.supervisorId, request.isHead);
                var result = await employeeRepository.CreateEmployee(employee);

                //Map Employee to EmployeeResponse
                var employeeResponse = result.ToEmployeeResponse();

                return employeeResponse;
            }
            catch (Exception e)
            {
                return EmployeeErrors.CreateEmployee(e.Message);
            }
        }
    }
}