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
}
