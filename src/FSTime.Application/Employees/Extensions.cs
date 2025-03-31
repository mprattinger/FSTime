using FSTime.Application.Users;
using FSTime.Contracts.Employees;
using FSTime.Domain.EmployeeAggregate;

namespace FSTime.Application.Employees;

public static class Extensions
{
    public static EmployeeResponse ToEmployeeResponse(this Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            CompanyId = employee.CompanyId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleName = employee.MiddleName,
            EmployeeCode = employee.EmployeeCode,
            EntryDate = employee.EntryDate,
            WorkplanId = employee.WorkplanId,
            User = employee.User?.ToUserResponse(),
            Supervisor = employee.Supervisor?.ToEmployeeResponse(),
            IsHead = employee.IsHead
        };
    }
}