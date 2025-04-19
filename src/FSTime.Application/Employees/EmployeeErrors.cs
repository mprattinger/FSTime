using ErrorOr;

namespace FSTime.Application.Employees;

public static class EmployeeErrors
{
    public static Error Get_All_Employees(string error)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.QUERY.ALL.GEN_ERROR",
            $"Error retrieving all employees: {error}");
    }

    public static Error Get_Employee(Guid id, string error)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.QUERY.ONE.GEN_ERROR",
            $"Error retrieving employee {id}: {error}");
    }

    public static Error Get_Employee_NotFound(Guid id)
    {
        return Error.NotFound("EMPLOYEE_HANDLER.QUERY.ONE.NOT_FOUND",
            $"Employee {id} not found");
    }

    public static Error CreateEmployee(string error)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.COMMAND.CREATE.GEN_ERROR",
            $"Error creating employee: {error}");
    }

    public static Error CreateEmployee_NoCompany(Guid companyId)
    {
        return Error.NotFound("EMPLOYEE_HANDLER.COMMAND.CREATE.NO_COMPANY",
            $"Company {companyId} not found");
    }

    public static Error AssignUser(string error)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.COMMAND.ASSIGN_USER.GEN_ERROR",
            $"Error assigning user to employee: {error}");
    }

    public static Error Get_Employee_User_NotFound(Guid id)
    {
        return Error.NotFound("EMPLOYEE_HANDLER.QUERY.ASSIGN_USER.USER_NOT_FOUND",
            $"User {id} not found");
    }

    public static Error SetSupervisor(string error)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.COMMAND.SET_SUPERVISOR.GEN_ERROR",
            $"Error assigning supervisor to employee: {error}");
    }

    public static Error Set_Supervisor_Employee_NotFound(Guid id)
    {
        return Error.NotFound("EMPLOYEE_HANDLER.QUERY.SET_SUPERVISOR.EMPLOYEE_NOT_FOUND",
            $"Employee {id} not found");
    }

    public static Error SetIsHead(string error)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.COMMAND.SET_HEAD.GEN_ERROR",
            $"Error setting employee as head: {error}");
    }

    public static Error HeadAlreadyExists(Guid id)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.COMMAND.SET_HEAD.HEAD_ALREADY_EXISTS",
            $"Employee {id} is already head");
    }

    public static Error SetEntryDate(string error)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.COMMAND.SET_ENTRY_DATE.GEN_ERROR",
            $"Error setting entry date to employee: {error}");
    }

    public static Error AddWorkschedule(string error)
    {
        return Error.Conflict("EMPLOYEE_HANDLER.COMMAND.ADD_WORKSCHEDULE.GEN_ERROR",
            $"Error setting workschedule to employee: {error}");
    }

    public static Error EmployeeNotInTenant(Guid employeeId)
    {
        return Error.NotFound("EMPLOYEE_HANDLER.COMMAND.ASSIGN_USER.EMPLOYEE_NOT_IN_TENANT",
            $"Employee {employeeId} not in tenant");
    }
}