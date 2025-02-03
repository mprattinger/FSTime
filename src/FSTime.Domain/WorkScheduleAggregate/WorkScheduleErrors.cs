using System;
using ErrorOr;

namespace FSTime.Domain.WorkScheduleAggregate;

public static class WorkScheduleErrors
{
  public static Error WorkingHoursTooHigh(float hours, float maxHours) => Error.Conflict("WORK_SCHEDULE.HOURS_TOO_HIGH", $"The hours per day exceed the maximum allowed hours per day ({hours} > {maxHours})");

  public static Error WorkingWeekHoursTooHigh(float hours, float maxHours) => Error.Conflict("WORK_SCHEDULE.WEEK_HOURS_TOO_HIGH", $"The hours per week exceed the maximum allowed hours per week ({hours} > {maxHours})");
}
