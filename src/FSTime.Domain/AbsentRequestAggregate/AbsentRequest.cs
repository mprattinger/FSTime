using System;
using FSTime.Domain.Common;

namespace FSTime.Domain.AbsentRequestAggregate;

public class AbsentRequest : Entity
{
    private readonly Guid _employeeId;
    private readonly Guid _createdBy;
    private readonly string _status = "PENDING";
    private readonly TimeRange _timeRange;
    private readonly string _absentType = "VACATION";

    public AbsentRequest(Guid? id = null)
    : base(id ?? Guid.NewGuid())
    {

    }
}
