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
    /// <summary>
    /// Class to config DI
    /// </summary>
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

        /// <summary>
        /// Method to add ID repositories
        /// </summary>
        /// <param name="services"></param>
        private static void AddDIRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IShopFoodLogger<>), typeof(ShopFoodLogger<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IFoodCatalogRepository), typeof(FoodCatalogRepository));
            services.AddScoped(typeof(IFoodOrderRepository), typeof(FoodOrderRepository));
        }

        /// <summary>
        /// Method to add ID business logic
        /// </summary>
        /// <param name="services"></param>
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
