using ComicManagerClean.Infrastructure.Services.Security.Contracts;

namespace ComicManagerClean.Api.Middleware;

public class JwtTokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    //private readonly IJwtTokenService _jwtTokenService;

    public JwtTokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _config = configuration;
        //_jwtTokenService = jwtTokenService;
    }

    public async Task Invoke(HttpContext httpContext, IJwtTokenService jwtTokenService)
    {
        string token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        Domain.Entities.User user = jwtTokenService.ValidateToken(token, _config.GetValue<string>("Jwt:Key"));

        // If token is valid, attach corresponding Domain user entity to the context
        if (user != null)
        {
            httpContext.Items["User"] = user;
        }

        await _next(httpContext);
    }
}


public static class JwtTokenValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtTokenValidationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtTokenValidationMiddleware>();
    }
}