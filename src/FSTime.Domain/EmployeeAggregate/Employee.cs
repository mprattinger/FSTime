using System;
using ErrorOr;
using FSTime.Domain.Common;

namespace FSTime.Domain.EmployeeAggregate;

public class Employee : AggregateRoot
{
    private readonly bool _isActive;
    private Guid? _userId;
    private Guid _workScheduleId;
    private readonly DateOnly _entryDate;
    private readonly int _vacationDaysPerYear;
    private Guid _companyId;
    
    public Employee(Guid companyId, DateOnly entryDate, int vacationDays, Guid? id = null)
    : base(id ?? Guid.NewGuid())
    {
        _companyId = companyId;
        _isActive = true;
        _entryDate = entryDate;
        _vacationDaysPerYear = vacationDays;
    }

    public void AssignUser(Guid userId)
    {
        _userId = userId;
    }

    public void AddWorkSchedule(Guid workScheduleId)
    {
        _workScheduleId = workScheduleId;
    }

    public bool IsActive()
    {
        return _isActive && _workScheduleId != Guid.Empty;
    }
}
