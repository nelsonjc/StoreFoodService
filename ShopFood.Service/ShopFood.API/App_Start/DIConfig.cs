using ShopFood.Application.Implements;
using ShopFood.Application.Wrappers;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Interfaces.Application.Wrappers;
using ShopFood.Domain.Interfaces.Logger;
using ShopFood.Domain.Interfaces.Repository;
using ShopFood.Domain.Utils.Logger;
using ShopFood.Infraestructure.Repositories;

namespace ShopFood.API.App_Start
{
    internal static class DIConfig
    {
        /// <summary>
        /// Add dependencies injection configuration
        /// </summary>
        /// <param name="services"></param>
        internal static void AddDIConfig(this IServiceCollection services)
        {
            AddDIRepository(services);
            AddDIBL(services);
        }

        private static void AddDIRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IShopFoodLogger<>), typeof(ShopFoodLogger<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IFoodCatalogRepository), typeof(FoodCatalogRepository));
            services.AddScoped(typeof(IFoodOrderRepository), typeof(FoodOrderRepository));
        }

        private static void AddDIBL(IServiceCollection services)
        {
            services.AddScoped(typeof(IPasswordHelper), typeof(PasswordHelperWrapper));
            services.AddScoped(typeof(ISecurityBL), typeof(SecurityBL));
            services.AddScoped(typeof(IUserBL), typeof(UserBL));
            services.AddScoped(typeof(IFoodCatalogBL), typeof(FoodCatalogBL));
            services.AddScoped(typeof(IFoodOrderBL), typeof(FoodOrderBL));
        }
    }
}
