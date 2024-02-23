using ComicManagerClean.Application.Common.Behaviors;
using FluentValidation;
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

        return services;
    }
}
