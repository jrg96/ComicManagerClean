using FluentValidation;
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

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
