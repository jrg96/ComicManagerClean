using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ComicManagerClean.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection UseInfrastructureProviders(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
