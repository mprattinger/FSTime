namespace FSTime.Contracts.Common.Exceptions.Employees;

public class SupervisorNotFoundException : Exception
{
    public SupervisorNotFoundException(Guid id):base($"Couldn't find supervisor {id}")
    {
        
    }
}