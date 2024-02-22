using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Infrastructure.Repositories;
using ComicManagerClean.Infrastructure.Repositories.Commands;
using ComicManagerClean.Infrastructure.Repositories.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ComicManagerClean.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection UseInfrastructureProviders(this IServiceCollection services)
    {
        services.AddScoped<IUserCommandRepository, UserCommandRepository>();
        services.AddScoped<IUserQueryRepository, UserQueryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
