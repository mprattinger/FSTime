using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Application;

public static class Extensions
{
    public static ServiceCollection AddApplication(this ServiceCollection services)
    {
        services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(Extensions)));
        services.AddValidatorsFromAssembly(typeof(Extensions).Assembly);
        return services;
    }
}
