// Copyright (C) TBC Bank. All Rights Reserved.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Todo.api.infrastructure.Auth;

public class JwtHelper
{
    public static string GenerateToken(string userName, string id, IOptions<AuthConfig> options)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(options.Value.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim("UserName", userName),
                new Claim("Id", id)
            }),
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audiance,
            Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }

    public static Dictionary<string, string> ParseClaims(HttpRequest request)
    {
        string result = request.Headers["Authorization"];
        string token = result.Substring("Bearer ".Length).Trim();
        var handler = new JwtSecurityTokenHandler();
        var parsedToken = handler.ReadJwtToken(token);
        var claims = parsedToken.Claims;
        var claimsDict = new Dictionary<string, string>();

        foreach (var claim in claims)
        {
            claimsDict.Add(claim.Type, claim.Value);
        }
        return claimsDict;
    }
}
