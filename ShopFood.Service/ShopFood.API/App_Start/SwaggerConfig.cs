using Microsoft.OpenApi.Models;
using ShopFood.Domain.Variables;
using System.Reflection;

namespace ShopFood.API.App_Start
{
    /// <summary>
    /// Class to config swagger 
    /// </summary>
    internal static class SwaggerConfig
    {
        /// <summary>
        /// Add swagger configuration
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns></returns>
        internal static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Swagger.SW_VERSION, GetOpenApiInfo());
                options.AddSecurityDefinition(Swagger.SW_SEC_DEF_SCHEME, GetOpenApiSecurityScheme());
                options.AddSecurityRequirement(GetOpenApiSecurityRequirement());
            });
        }

        /// <summary>
        /// Get Open API information
        /// </summary>
        /// <returns></returns>
        internal static OpenApiInfo GetOpenApiInfo()
        {
            return new OpenApiInfo
            {
                Title = Swagger.SW_TITLE,
                Version = Swagger.SW_VERSION,
                Description = Swagger.SW_DESCRIPTION,
                Contact = new OpenApiContact() { Name = Swagger.SW_NAME, Email = Swagger.SW_EMAIL, Url = null }
            };
        }

        /// <summary>
        /// Get Open API security scheme
        /// </summary>
        /// <returns></returns>
        internal static OpenApiSecurityScheme GetOpenApiSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Description = Swagger.SW_SEC_DEF_DESCRIPTION,
                Name = Swagger.SW_SEC_NAME,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = Swagger.SW_SEC_DEF_SCHEME
            };
        }

        /// <summary>
        /// Get Open API security requirements
        /// </summary>
        /// <returns></returns>
        internal static OpenApiSecurityRequirement GetOpenApiSecurityRequirement()
        {
            return new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = Swagger.SW_SEC_DEF_SCHEME
                        }
                    }, new List<string>()
                }
            };
        }

        /// <summary>
        /// Get comments path file
        /// </summary>
        /// <returns></returns>
        internal static string GetCommentsPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string commentsFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}{Swagger.SW_COMMENT_PATH_EXT}";
            return Path.Combine(baseDirectory, commentsFileName);
        }

        /// <summary>
        /// Use swagger configuration
        /// </summary>
        /// <param name="app"></param>
        internal static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Swagger.SW_URL_JSON, Swagger.SW_TITLE);
            });
        }
    }
}
