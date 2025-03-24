using FSTime.Domain.Common;
using FSTime.Domain.CompanyAggregate;
using FSTime.Domain.UserAggregate;

namespace FSTime.Domain.EmployeeAggregate;

public class Employee : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public Company? Company { get; private set; }
    
    public string? EmployeeCode { get; }
    public string FirstName { get; } = null!;
    public string? MiddleName { get; } 
    public string LastName { get; } = null!;
    
    public DateTime? EntryDate { get; private set; }

    public Guid? WorkplanId { get;}

    public Guid? UserId { get; private set; }
    public User? User { get; private set; }
    
    public bool IsHead {get; private set;}
    public Guid? SupervisorId { get; private set; }
    public Employee? Supervisor { get; private set; }

    public Employee(Guid companyId, string firstName, string lastName, string? middleName = "", Guid? supervisorId = null, Guid? id = null)
    :base(id ?? Guid.CreateVersion7())
    {
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        
        if (supervisorId is not null)
        {
            SupervisorId = supervisorId;    
        }
    }

    public void SetEntryDate(DateTime date)
    {
        EntryDate = date;
    }
    
    public bool IsActive()
    {
        if(EntryDate is null || WorkplanId is null)
        {
            return false;
        }
        
        if(SupervisorId is null && IsHead == false)
        {
            return false;
        }
        
        return true;
    }

    public void AssignUser(User user)
    {
        UserId = user.Id;
        User = user;
    }
    
    public void UnassignUser()
    {
        UserId = null;
        User = null;
    }
    
    public void SetSupervisor(Employee supervisor)
    {
        SupervisorId = supervisor.Id;
        Supervisor = supervisor;
        IsHead = false;
    }

    public void SetIsHead()
    {
        IsHead = true;
        Supervisor = null;
        SupervisorId = null;
    }
    
    private Employee()
    {
    }
}