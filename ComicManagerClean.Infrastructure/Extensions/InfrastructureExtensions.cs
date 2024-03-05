using ComicManagerClean.Application.Services.Security;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Infrastructure.Repositories;
using ComicManagerClean.Infrastructure.Repositories.Commands;
using ComicManagerClean.Infrastructure.Repositories.Queries;
using ComicManagerClean.Infrastructure.Services.Security;
using ComicManagerClean.Infrastructure.Services.Security.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ComicManagerClean.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection UseInfrastructureProviders(this IServiceCollection services)
    {
        services.AddScoped<IUserCommandRepository, UserCommandRepository>();
        services.AddScoped<IUserQueryRepository, UserQueryRepository>();

        services.AddScoped<ICharacterCommandRepository, CharacterCommandRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordSecurityService, PasswordSecurityService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
