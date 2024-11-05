using Application.Services.AuthServices;
using Application.Services.Repositories;
using Core.SecurityIdentity.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Services;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("NProjectConnection")));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFavoriteDomainRepository, FavoriteDomainRepository>();

        //Add Identity
        services.AddIdentity<AppUser, BaseRoleEntity>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<BaseDbContext>();


        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
            context.Database.Migrate();
        }

        return services;
    }
}
