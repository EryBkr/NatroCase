using Application.Features.FavoriteDomains.Notifications.DomainAddedNotifications;
using Application.Features.FavoriteDomains.Notifications.DomainUpdatedNotifications;
using Application.Features.FavoriteDomains.Rules;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Add Mapper Profiles
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Business Rules
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        //Add Fluent Validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register MediatR services
        services.AddMediatR(configuration =>
        {
            // Register services from the current executing assembly
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            //Add Validation Behavior
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));

            //Add Transaction Behavior
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));

            //Add Logging Behavior
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            //Add Cache Behavior
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));

            //Add Cache Remover Behavior
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
        });

        //Logging
        //services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<LoggerServiceBase, MsSqlLogger>();

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
       this IServiceCollection services,
       Assembly assembly,
       Type type,
       Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
   )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);

            else
                addWithLifeCycle(services, type);
        return services;
    }
}
