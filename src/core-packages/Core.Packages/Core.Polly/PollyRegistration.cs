using Core.Polly.Policy;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Polly;

public static class PollyRegistration
{
    public static IServiceCollection AddAPollyRegistrationServices(this IServiceCollection services)
    {
        services.AddSingleton<PolicyProvider>();
        return services;
    }
}
