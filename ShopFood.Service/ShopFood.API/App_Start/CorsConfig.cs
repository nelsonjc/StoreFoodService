namespace ShopFood.API.App_Start
{
    internal static class CorsConfig
    {
        internal static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("ShopFoodPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            return services;
        }

        internal static IApplicationBuilder UseCorsDocumentation(this IApplicationBuilder app)
        {
            app.UseCors("ShopFoodPolicy");
            return app;
        }
    }
}
