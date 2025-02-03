using System;
using ErrorOr;
using FSTime.Domain.Common;

namespace FSTime.Domain.WorkScheduleAggregate;

public class WorkSchedule : AggregateRoot
{

    const float MAX_WEEKLY_WORKING_HOURS = 38.5F;
    const float MAX_DAYLY_WORKING_HOURS = 10.0F;

    internal List<WorkHoursPerDay> _workSchedules = [];

    private WorkSchedule(Guid? id = null)
    : base(id ?? Guid.NewGuid())
    {
    }

    public static ErrorOr<WorkSchedule> CreateWorkSchedule(float weeklyWorkingHours, WorkDay[] workDays)
    {
        var instance = new WorkSchedule();

        if (weeklyWorkingHours > MAX_WEEKLY_WORKING_HOURS)
        {
            return WorkScheduleErrors.WorkingWeekHoursTooHigh(weeklyWorkingHours, MAX_WEEKLY_WORKING_HOURS);
        }

        var hoursPerDay = weeklyWorkingHours / workDays.Length;

        if (hoursPerDay > MAX_DAYLY_WORKING_HOURS)
        {
            return WorkScheduleErrors.WorkingHoursTooHigh(hoursPerDay, MAX_DAYLY_WORKING_HOURS);
        }

        instance._workSchedules = workDays.Select(x => new WorkHoursPerDay(x, hoursPerDay)).ToList();

        return instance;
    }

    public static ErrorOr<WorkSchedule> CreateWorkSchedule(List<WorkHoursPerDay> workSchedules)
    {
        var weeklyWorkingHours = workSchedules.Sum(x => x.Hours);
        var maxDaylyHours = workSchedules.Max(x => x.Hours);

        if (weeklyWorkingHours > MAX_WEEKLY_WORKING_HOURS)
        {
            return WorkScheduleErrors.WorkingWeekHoursTooHigh(weeklyWorkingHours, MAX_WEEKLY_WORKING_HOURS);
        }

        if (maxDaylyHours > MAX_DAYLY_WORKING_HOURS)
        {
            return WorkScheduleErrors.WorkingHoursTooHigh(maxDaylyHours, MAX_DAYLY_WORKING_HOURS);
        }

        var instance = new WorkSchedule();
        instance._workSchedules = workSchedules;

        return instance;
    }
}