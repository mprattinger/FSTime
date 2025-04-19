using FSTime.Contracts.WorkSchedule;
using FSTime.Domain.Common.ValueObjects;

namespace FSTime.Application.Workschedules;

public static class Extensions
{
    public static EmployeeWorkscheduleResponse ToWorkscheduleResponse(this EmployeeWorkschedule employee)
    {
        var ret = new EmployeeWorkscheduleResponse
        {
            WorkscheduleId = employee.WorkscheduleId,
            From = DateOnly.FromDateTime(employee.From.ToLocalTime()),
            Description = employee.Workschedule.Description,
            Monday = employee.Workschedule.Monday,
            Tuesday = employee.Workschedule.Tuesday,
            Wednesday = employee.Workschedule.Wednesday,
            Thursday = employee.Workschedule.Thursday,
            Friday = employee.Workschedule.Friday,
            Saturday = employee.Workschedule.Saturday,
            Sunday = employee.Workschedule.Sunday
        };

        if (employee.To is not null) ret.To = DateOnly.FromDateTime(employee.To.Value);

        return ret;
    }
}