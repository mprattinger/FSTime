using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Common.Exceptions.Employees;
using FSTime.Domain.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;

namespace FSTime.Infrastructure.Persistence.Repositories;

public class EmployeeRepository(FSTimeDbContext context) : IEmployeeRepository
{
    public Task<List<Employee>> GetEmployees(Guid companyId)
    {
        return context.Employees.Where(x => x.CompanyId == companyId).ToListAsync();
    }

    public async Task<Employee?> GetEmployee(Guid id)
    {
        return await context
            .Employees
            .FindAsync(id);
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> GetEmployeeByUserId(Guid userId)
    {
        return await context.Employees.FirstOrDefaultAsync(e => e.UserId == userId);
    }

    public async Task<Employee> AssignUserToEmployee(Guid employeeId, Guid userId)
    {
        var employee = await context.Employees.FindAsync(employeeId);
        if (employee == null) throw new EmployeeNotFoundException(employeeId);

        var user = await context.Users.FindAsync(userId);
        if (user == null) throw new EmployeeNotFoundException(userId);

        employee.AssignUser(user);
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UnassignUser(Guid employeeId)
    {
        var employee = await context.Employees.FindAsync(employeeId);
        if (employee == null) throw new EmployeeNotFoundException(employeeId);

        employee.UnassignUser();
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> SetSupervisor(Guid employeeId, Guid supervisorId)
    {
        var employee = await context.Employees.FindAsync(employeeId);
        if (employee == null) throw new EmployeeNotFoundException(employeeId);

        var supervisor = await context.Employees.FindAsync(supervisorId);
        if (supervisor == null) throw new SupervisorNotFoundException(supervisorId);

        employee.SetSupervisor(supervisor);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> SetIsHead(Guid employeeId)
    {
        var employee = await context.Employees.FindAsync(employeeId);
        if (employee == null) throw new EmployeeNotFoundException(employeeId);

        employee.SetIsHead();
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> GetHead()
    {
        return await context.Employees.FirstOrDefaultAsync(x => x.IsHead);
    }
}