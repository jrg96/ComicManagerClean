using ComicManagerClean.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace ComicManagerClean.Infrastructure.Services.Security.Contracts;

public interface IJwtTokenService
{
    string GetJwtToken(double longevityMinutes, string issuer, string securityKey, string securityAlgorithm, Dictionary<string, string> claims);
    JwtSecurityToken DecryptToken(string token);
    User? ValidateToken(string token, string securityKey);
}
