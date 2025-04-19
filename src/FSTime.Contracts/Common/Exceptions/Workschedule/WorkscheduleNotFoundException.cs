namespace FSTime.Contracts.Common.Exceptions.Workschedule;

public class WorkscheduleNotFoundException : Exception
{
    public WorkscheduleNotFoundException(Guid workscheduleId) : base($"Couldn't find workschedule {workscheduleId}")
    {
    }
}