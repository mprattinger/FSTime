using System;

namespace FSTime.Domain.Tests;

public class BookingsTests
{
  [Fact]
  public void CreateBooking()
  {
    var booking = new Booking(Guid.NewGuid(), DateTime.Now);

    Assert.NotNull(booking);
  }
}
