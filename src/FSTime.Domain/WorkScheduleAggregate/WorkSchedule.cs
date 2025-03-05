using System;
using ErrorOr;
using FSTime.Domain.Common;

namespace FSTime.Domain.WorkScheduleAggregate;

public class WorkSchedule : AggregateRoot
{
    const float MAX_WEEKLY_WORKING_HOURS = 38.5F; //TODO -> move to configuration
    const float MAX_DAYLY_WORKING_HOURS = 10.0F; //TODO -> move to configuration

    internal Guid _companyId;
    
    internal List<WorkHoursPerDay> _workSchedules = [];

    internal string _description = "";
    
    private WorkSchedule(Guid companyId, Guid? id = null)
    : base(id ?? Guid.NewGuid())
    {
        _companyId = companyId;
    }

    public static ErrorOr<WorkSchedule> CreateWorkSchedule(Guid companyId, float weeklyWorkingHours, WorkDay[] workDays)
    {
        var instance = new WorkSchedule(companyId);

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

    public static ErrorOr<WorkSchedule> CreateWorkSchedule(Guid companyId, List<WorkHoursPerDay> workSchedules)
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

        var instance = new WorkSchedule(companyId);
        instance._workSchedules = workSchedules;

        return instance;
    }

    private WorkSchedule() {}
}