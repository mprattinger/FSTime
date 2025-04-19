using ErrorOr;
using MediatR;

namespace FSTime.Application.Authorization.Queries;

public static class GetActions
{
    public record Query(string Group) : IRequest<ErrorOr<List<string>>>;
}