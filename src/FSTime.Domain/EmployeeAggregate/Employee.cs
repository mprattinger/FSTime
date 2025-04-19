using FSTime.Domain.Common;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.CompanyAggregate;
using FSTime.Domain.UserAggregate;

namespace FSTime.Domain.EmployeeAggregate;

public class Employee : AggregateRoot
{
    public Employee(Guid companyId, string firstName, string lastName, string? middleName = "",
        Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public Employee(Guid companyId, string firstName, string lastName, string middleName,
        Guid supervisorId, Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        SupervisorId = supervisorId;
    }

    public Employee(Guid companyId, string firstName, string lastName, string middleName,
        bool isHead, Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        IsHead = isHead;
    }

    public Employee(Guid companyId, string firstName, string lastName, string? middleName = null,
        DateTime? entryDate = null, Guid? supervisorId = null, bool? isHead = null, Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;

        if (entryDate is not null) EntryDate = entryDate;
        if (supervisorId is not null) SupervisorId = supervisorId;
        if (isHead is not null) IsHead = isHead.Value;
    }

    public Employee(Guid companyId, string firstName, string lastName, string middleName,
        DateTime entryDate, Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        EntryDate = entryDate;
    }

    private Employee()
    {
    }

    public Guid CompanyId { get; private set; }
    public Company? Company { get; private set; }

    public string? EmployeeCode { get; }
    public string FirstName { get; } = null!;
    public string? MiddleName { get; }
    public string LastName { get; } = null!;

    public DateTime? EntryDate { get; private set; }

    public Guid? UserId { get; private set; }
    public User? User { get; private set; }

    public bool IsHead { get; private set; }
    public Guid? SupervisorId { get; private set; }
    public Employee? Supervisor { get; private set; }

    public List<EmployeeWorkschedule> Workschedules { get; } = [];

    public bool IsActive
    {
        get
        {
            if (EntryDate is null || GetActiveWorkschedule() is null) return false;

            if (SupervisorId is null && IsHead == false) return false;

            return true;
        }
    }

    public void SetEntryDate(DateTime date)
    {
        EntryDate = date;
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

    public EmployeeWorkschedule? GetActiveWorkschedule()
    {
        return Workschedules
            .FirstOrDefault(x => x.From <= DateTime.UtcNow && (x.To is null || x.To > DateTime.UtcNow));
    }

    public void AddWorkschedule(EmployeeWorkschedule workschedule)
    {
        //First set the old workschedule to inactive
        var old = Workschedules.FirstOrDefault(x => x.To is null);
        if (old is not null) old.SetTo(workschedule.From.AddDays(-1));
        Workschedules.Add(workschedule);
    }
}