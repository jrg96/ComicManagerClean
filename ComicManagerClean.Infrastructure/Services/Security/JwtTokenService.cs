using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Shared.Enums;
using ComicManagerClean.Infrastructure.Constants;
using ComicManagerClean.Infrastructure.Services.Security.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComicManagerClean.Infrastructure.Services.Security;

public class JwtTokenService : IJwtTokenService
{
    public JwtSecurityToken DecryptToken(string tokenData)
    {
        string token = tokenData;
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(token);
    }

    public string GetJwtToken(double longevityMinutes, string issuer, string securityKey, string securityAlgorithm, Dictionary<string, string> claimsData)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        SigningCredentials credentials = new SigningCredentials(key, securityAlgorithm);

        List<Claim> claims = new List<Claim>();

        foreach(KeyValuePair<string, string> entry in claimsData)
        {
            claims.Add(new Claim(entry.Key, entry.Value));
        }

        SecurityToken token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(longevityMinutes),
            signingCredentials: credentials 
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public User? ValidateToken(string token, string securityKey)
    {
        if (token == null)
        {
            return null;
        }

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        TokenValidationParameters validationParameters = new TokenValidationParameters 
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
        };


        try
        {
            // If token is not valid, ValidateToken will throw an exception
            SecurityToken securityToken;
            handler.ValidateToken(token, validationParameters, out securityToken);

            // If everything is ok, recreate User entity
            RolesEnum role;
            JwtSecurityToken jwtToken = (JwtSecurityToken)securityToken;
            Enum.TryParse(jwtToken.Claims.First(claim => claim.Type == JwtClaimConstants.USER_ROLE_CLAIM).Value, true, out role);

            User user = new User() 
            {
                Id = new Guid(jwtToken.Claims.First(claim => claim.Type == JwtClaimConstants.USER_ID_CLAIM).Value),
                Role = role
            };

            return user;

        }
        catch(Exception e)
        {
            return null;
        }
    }
}
