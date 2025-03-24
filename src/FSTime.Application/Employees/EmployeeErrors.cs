using ErrorOr;

namespace FSTime.Application.Employees;

public static class EmployeeErrors
{
    public static Error Get_All_Employees(string error) => Error.Conflict("EMPLOYEE_HANDLER.QUERY.ALL.GEN_ERROR",
        $"Error retrieving all employees: {error}");
    
    public static Error Get_Employee(Guid id, string error) => Error.Conflict("EMPLOYEE_HANDLER.QUERY.ONE.GEN_ERROR",
        $"Error retrieving employee {id}: {error}");
    
    public static Error Get_Employee_NotFound(Guid id) => Error.NotFound("EMPLOYEE_HANDLER.QUERY.ONE.NOT_FOUND",
        $"Employee {id} not found");
}