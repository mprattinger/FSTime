using System;
using ErrorOr;
using FSTime.Domain.Common;
using Throw;

namespace FSTime.Domain.AbsentRequestAggregate;

public class TimeRange : ValueObject
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }

    public TimeRange(DateTime from, DateTime to)
    {
        From = from.Throw().IfGreaterThanOrEqualTo(to);
        To = to;
    }

    public bool OverlapsWith(TimeRange other)
    {
        if (From >= other.To) return false;
        if (other.From >= To) return false;

        return true;
    }

    public static ErrorOr<TimeRange> FromDateTimes(DateTime from, DateTime to)
    {
        if (from >= to)
        {
            return Error.Validation();
        }

        return new TimeRange(from, to);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return From;
        yield return To;
    }
}
