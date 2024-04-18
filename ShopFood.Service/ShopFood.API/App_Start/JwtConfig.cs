using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ShopFood.API.App_Start
{
    internal static class JwtConfig
    {
        /// <summary>
        /// Add JWT documentation
        /// </summary>
        /// <param name="services">Parameter service type of IServiceCollection</param>
        /// <returns></returns>
        internal static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var secret = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateIssuer = true,
                            ValidIssuer = issuer,
                            ValidateAudience = true,
                            ValidAudience = audience,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            RequireExpirationTime = true,
                        };
                    });

            services.AddAuthorization();
            return services;
        }
    }
}
