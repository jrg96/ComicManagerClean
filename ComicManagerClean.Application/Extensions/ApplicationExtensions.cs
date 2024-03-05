using ComicManagerClean.Application.Common.Behaviors;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ComicManagerClean.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection UseApplicationProviders(this IServiceCollection services)
    {
        Assembly assembly = typeof(ApplicationExtensions).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandQueryValidationBehavior<,>));
        services.AddValidatorsFromAssembly(assembly);

        // Configuration for mapster
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
