using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;
using FSTime.Domain.EmployeeAggregate;

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
        : ICommand<EmployeeResponse>;

    internal sealed class Handler(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
        : ICommandHandler<Command, EmployeeResponse>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken)
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
    }
}