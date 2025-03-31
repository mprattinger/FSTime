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
        WeekWorkTime = weekWorkTime;
        WeekWorkDays = weekWorkDays;
    }

    // public WorkSchedule(Guid companyId, string description, double? monday = null, double? tuesday = null,
    //     double? wednesday = null,
    //     double? thursday = null, double? friday = null, double? saturday = null, double? sunday = null, Guid? id = null)
    //     : base(id ?? Guid.CreateVersion7())
    // {
    //     CompanyId = companyId;
    //     Description = description;
    //     Monday = monday;
    //     Tuesday = tuesday;
    //     Wednesday = wednesday;
    //     Thursday = thursday;
    //     Friday = friday;
    //     Saturday = saturday;
    //     Sunday = sunday;
    // }

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

    public double? WeekWorkTime { get; private set; }
    public int? WeekWorkDays { get; private set; }

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