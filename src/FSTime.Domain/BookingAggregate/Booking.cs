using System;
using FSTime.Domain.Common;

namespace FSTime.Domain.BookingAggregate;

public class Booking : AggregateRoot
{
    private readonly Guid _employeeId;
    private readonly DateTime _bookingTime;
    private readonly string _bookingType = "NORMAL";

    public Booking(Guid employeeId, DateTime bookingTime, string? bookingType = null, Guid? id = null)
    : base(id ?? Guid.NewGuid())
    {
        _bookingTime = bookingTime;
        _bookingType = bookingType ?? _bookingType;
    }
}
