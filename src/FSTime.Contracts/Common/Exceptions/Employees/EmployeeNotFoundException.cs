namespace FSTime.Contracts.Common.Exceptions.Employees;

public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException(Guid employeeId):base($"Couldn't find employee {employeeId}")
    {
        
    }
}