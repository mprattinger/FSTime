using System;
using ErrorOr;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Domain.Tests.TestUtils;

public static class WorkScheduleFactory
{
  public static ErrorOr<WorkSchedule> CreateValidWeekHoursSchedule()
  {
    return WorkSchedule.CreateWorkSchedule(
        Guid.NewGuid(),
        38.5F,
        new WorkDay[] { WorkDay.Monday, WorkDay.Tuesday, WorkDay.Wednesday, WorkDay.Thursday, WorkDay.Friday });
  }

  public static ErrorOr<WorkSchedule> CreateWeeklyHoursExceedingSchedule()
  {
    return WorkSchedule.CreateWorkSchedule(
        Guid.NewGuid(),
        39F,
        new WorkDay[] { WorkDay.Monday, WorkDay.Tuesday, WorkDay.Wednesday, WorkDay.Thursday, WorkDay.Friday });
  }

  public static ErrorOr<WorkSchedule> CreateDaylyHoursExceedingSchedule()
  {
    return WorkSchedule.CreateWorkSchedule(
        Guid.NewGuid(),
        33F,
        new WorkDay[] { WorkDay.Monday, WorkDay.Tuesday, WorkDay.Wednesday });
  }

  public static ErrorOr<WorkSchedule> CreateValidDayHoursSchedule()
  {
    return WorkSchedule.CreateWorkSchedule(
        Guid.NewGuid(),
        new List<WorkHoursPerDay>
        {
            new WorkHoursPerDay(WorkDay.Monday, 8.12F),
            new WorkHoursPerDay(WorkDay.Tuesday, 8.12F),
            new WorkHoursPerDay(WorkDay.Wednesday, 8.12F),
            new WorkHoursPerDay(WorkDay.Thursday, 8.12F),
            new WorkHoursPerDay(WorkDay.Friday, 5.48F)
        });
  }

  public static ErrorOr<WorkSchedule> CreateInValidDayHoursSchedule_WeekHours()
  {
    return WorkSchedule.CreateWorkSchedule(
        Guid.NewGuid(),
        new List<WorkHoursPerDay>
        {
            new WorkHoursPerDay(WorkDay.Monday, 8.12F),
            new WorkHoursPerDay(WorkDay.Tuesday, 8.12F),
            new WorkHoursPerDay(WorkDay.Wednesday, 8.12F),
            new WorkHoursPerDay(WorkDay.Thursday, 8.12F),
            new WorkHoursPerDay(WorkDay.Friday, 8.12F)
        });
  }

  public static ErrorOr<WorkSchedule> CreateInValidDayHoursSchedule_DayHours()
  {
    return WorkSchedule.CreateWorkSchedule(
        Guid.NewGuid(),
        new List<WorkHoursPerDay>
        {
            new WorkHoursPerDay(WorkDay.Monday, 8.12F),
            new WorkHoursPerDay(WorkDay.Tuesday, 8.12F),
            new WorkHoursPerDay(WorkDay.Thursday, 11F),
            new WorkHoursPerDay(WorkDay.Friday, 5.48F)
        });
  }
}
