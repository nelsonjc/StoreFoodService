using ShopFood.Application.Implements;
using ShopFood.Application.Wrappers;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Interfaces.Application.Wrappers;
using ShopFood.Domain.Interfaces.Logger;
using ShopFood.Domain.Interfaces.Repository;
using ShopFood.Domain.Utils.Logger;
using ShopFood.Infraestructure;
using ShopFood.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            
            
        }

        private static void AddDIBL(IServiceCollection services)
        {
            services.AddScoped(typeof(ISecurityBL), typeof(SecurityBL));
            services.AddScoped(typeof(IPasswordHelper), typeof(PasswordHelperWrapper));
        }
    }
}
