namespace FSTime.Infrastructure.Common.Interfaces;

public interface IPermissionService
{
    Task<bool> HasPermission(Guid userId, string group, string action);
}