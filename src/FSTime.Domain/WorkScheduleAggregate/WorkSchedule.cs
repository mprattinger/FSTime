using FSTime.Domain.Common;
using FSTime.Domain.CompanyAggregate;

namespace FSTime.Domain.WorkScheduleAggregate;

public class WorkSchedule : AggregateRoot
{
    public WorkSchedule(Guid companyId, string description, double weekWorkTime, int weekWorkDays, Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        CompanyId = companyId;
        Description = description;

        var timePerDay = weekWorkTime / weekWorkDays;

        for (var i = 0; i < weekWorkDays; i++)
            switch (i)
            {
                case 0:
                    Monday = timePerDay;
                    break;
                case 1:
                    Tuesday = timePerDay;
                    break;
                case 2:
                    Wednesday = timePerDay;
                    break;
                case 3:
                    Thursday = timePerDay;
                    break;
                case 4:
                    Friday = timePerDay;
                    break;
                case 5:
                    Saturday = timePerDay;
                    break;
                case 6:
                    Sunday = timePerDay;
                    break;
            }
    }

    public WorkSchedule(Guid companyId, string description, Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        CompanyId = companyId;
        Description = description;
    }

    private WorkSchedule()
    {
    }

    public Guid CompanyId { get; }
    public Company? Company { get; } = null!;

    public string Description { get; } = null!;

    public double? Monday { get; private set; }
    public double? Tuesday { get; private set; }
    public double? Wednesday { get; private set; }
    public double? Thursday { get; private set; }
    public double? Friday { get; private set; }
    public double? Saturday { get; private set; }
    public double? Sunday { get; private set; }

    public void SetMonday(double? monday)
    {
        Monday = monday;
    }

    public void SetTuesday(double? tuesday)
    {
        Tuesday = tuesday;
    }

    public void SetWednesday(double? wednesday)
    {
        Wednesday = wednesday;
    }

    public void SetThursday(double? thursday)
    {
        Thursday = thursday;
    }

    public void SetFriday(double? friday)
    {
        Friday = friday;
    }

    public void SetSaturday(double? saturday)
    {
        Saturday = saturday;
    }

    public void SetSunday(double? sunday)
    {
        Sunday = sunday;
    }
}