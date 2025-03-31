using ErrorOr;

namespace FSTime.Application.Workschedules;

public static class WorkscheduleErrors
{
    public static Error Get_Workschedule(Guid id, string error)
    {
        return Error.Conflict("WORKSCHEDULE_HANDLER.QUERY.ONE.GEN_ERROR",
            $"Error retrieving workschedule {id}: {error}");
    }

    public static Error Get_Workschedules(string error)
    {
        return Error.Conflict("WORKSCHEDULE_HANDLER.QUERY.ALL.GEN_ERROR",
            $"Error retrieving workschedules: {error}");
    }

    public static Error Workschedule_NotFound(Guid id)
    {
        return Error.NotFound("WORKSCHEDULE_HANDLER.NOT_FOUND",
            $"Error workschedule {id} not found");
    }

    public static Error Create_Workschedule(string description, string error)
    {
        return Error.Conflict("WORKSCHEDULE_HANDLER.COMMAND.CREATE.GEN_ERROR",
            $"Error error creating workschedule {description}: {error}");
    }
}