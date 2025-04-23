using ErrorOr;
using FlintSoft.CQRS;
using FluentValidation;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;

namespace FSTime.Application.Authorization.Commands;

public static class SetPermission
{
    public record Command(Guid TenantId, Guid UserId, string Group, PermissionAction action)
        : IRequest<ErrorOr<List<Permission>>>;

    //We need to add validation here
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IPolicyInspector policyInspector)
        {
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Group).NotEmpty();

            var groups = policyInspector.GetGroups();

            // Check if the group is in the allowed group list
            RuleFor(x => x.Group)
                .Must((group, cancellationToken) =>
                   groups.Any(x => x == group.Group))
                .WithMessage("The specified group is not allowed.");
        }
    }

    internal sealed class Handler(IPermissionRepository permissionRepository, IValidator<Command> validator)
        : IRequestHandler<Command, ErrorOr<List<Permission>>>
    {
        public async Task<ErrorOr<List<Permission>>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return validationResult.Errors.ConvertAll(error =>
                        Error.Validation(error.PropertyName, error.ErrorMessage));

                var permission = new Permission(request.TenantId, request.UserId, request.Group, request.action);

                var permissions = await permissionRepository.SetPermission(permission);

                var permissionsList = await permissionRepository.GetPermissions(request.TenantId, request.UserId);
                return permissionsList;
            }
            catch (Exception e)
            {
                return AuthorizationErrors.Permissions_GenError(e.Message);
            }
        }
    }
}