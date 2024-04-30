// Copyright (C) TBC Bank. All Rights Reserved.

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Todo.api.infrastructure.Exstensions;

public static class AuthExstention
{
    public static void AddTokenAuth(this IServiceCollection services, IConfiguration config)
    {
        var key = Encoding.ASCII.GetBytes(config.GetValue<string>("AuthConfig:SecretKey"));

        services.AddAuthentication(x => {
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x => {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config.GetValue<string>("AuthConfig:Issuer"),
                ValidAudience = config.GetValue<string>("AuthConfig:Audiance"),
                IssuerSigningKey = new SymmetricSecurityKey(key),

            };
        });
    }
}
