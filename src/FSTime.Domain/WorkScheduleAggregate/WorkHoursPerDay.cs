using System;
using FSTime.Domain.Common;

namespace FSTime.Domain.WorkScheduleAggregate;

public enum WorkDay
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

public class WorkHoursPerDay : ValueObject
{
    public WorkDay WorkDay { get; init; }
    public float Hours { get; init; }

    public WorkHoursPerDay(WorkDay workDay, float hours)
    {
        WorkDay = workDay;
        Hours = hours;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return WorkDay;
        yield return Hours;
    }
}
