using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FSTime.Application;

public static class Extensions
{
    public static IHostApplicationBuilder? AddApplication(this IHostApplicationBuilder? host)
    {
        host?.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(Extensions)));
        host?.Services.AddValidatorsFromAssembly(typeof(Extensions).Assembly);
        return host;
    }
}
