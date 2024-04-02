using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VideoHosting_Back_end.Common.Configuration;

namespace VideoHosting_Back_end.Configurations;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = new AuthConfigurationOptions(configuration);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                // string that represents token creator
                options.TokenValidationParameters.ValidIssuer = jwtOptions.Issuer;

                // string that represents token consumer
                options.TokenValidationParameters.ValidAudience = jwtOptions.Audience;
                options.Audience = jwtOptions.Audience;

                // setting security key
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

                // security key validation enabled
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;
            });
    }
}
