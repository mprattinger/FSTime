using System;
using FluentAssertions;
using FSTime.Domain.Tests.TestUtils;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Domain.Tests;

public class WorkScheduleTests
{
  [Fact]
  public void CreateWorkSchedule_ValidWeekHoursSchedule()
  {
    var result = WorkScheduleFactory.CreateValidWeekHoursSchedule();

    result.IsError.Should().BeFalse();
  }

  [Fact]
  public void CreateWorkSchedule_InValidWeekHoursSchedule()
  {
    var result = WorkScheduleFactory.CreateWeeklyHoursExceedingSchedule();

    result.IsError.Should().BeTrue();
    result.FirstError.Should().Be(WorkScheduleErrors.WorkingWeekHoursTooHigh(39F, 38.5F));
  }

  [Fact]
  public void CreateWorkSchedule_InValidDayHoursSchedule()
  {
    var result = WorkScheduleFactory.CreateDaylyHoursExceedingSchedule();

    result.IsError.Should().BeTrue();
    result.FirstError.Should().Be(WorkScheduleErrors.WorkingHoursTooHigh(11F, 10F));
  }

  [Fact]
  public void CreateWorkSchedule_ValidDayHoursSchedule()
  {
    var result = WorkScheduleFactory.CreateValidDayHoursSchedule();

    result.IsError.Should().BeFalse();
  }

  [Fact]
  public void CreateWorkSchedule_InValidDayHoursSchedule_WeekHours()
  {
    var result = WorkScheduleFactory.CreateInValidDayHoursSchedule_WeekHours();

    result.IsError.Should().BeTrue();
    result.FirstError.Should().Be(WorkScheduleErrors.WorkingWeekHoursTooHigh(40.6F, 38.5F));
  }

  [Fact]
  public void CreateWorkSchedule_InValidDayHoursSchedule_DayHours()
  {
    var result = WorkScheduleFactory.CreateInValidDayHoursSchedule_DayHours();

    result.IsError.Should().BeTrue();
    result.FirstError.Should().Be(WorkScheduleErrors.WorkingHoursTooHigh(11F, 10F));
  }
}
