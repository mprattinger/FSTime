using FSTime.Domain.EmployeeAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetEmployees(Guid companyId);
    Task<Employee?> GetEmployee(Guid id);
    Task<Employee> CreateEmployee(Employee employee);
    Task<Employee> UpdateEmployee(Employee employee);

    Task<Employee?> GetEmployeeByUserId(Guid userId);

    Task<Employee> AssignUserToEmployee(Guid employeeId, Guid userId);
    Task<Employee> UnassignUser(Guid employeeId);

    Task<Employee?> SetSupervisor(Guid employeeId, Guid supervisorId);
    Task<Employee?> SetIsHead(Guid employeeId);
    Task<Employee?> GetHead();

    Task<Employee> AddWorkschedule(Guid employeeId, Guid workscheduleId, DateTime validFrom);
}