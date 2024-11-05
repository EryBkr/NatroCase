using Application.Services.RdapServices;
using Core.Polly;
using Infrastructure.Services.Rdap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddAInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAPollyRegistrationServices();

        services.AddHttpClient<IRdapService, RdapService>((provider, client) =>
        {
            RdapSettings rdapSettings = configuration.GetSection("RdapSettings").Get<RdapSettings>();
            client.BaseAddress = new Uri(rdapSettings.BaseUrl);
        });

        return services;
    }
}
