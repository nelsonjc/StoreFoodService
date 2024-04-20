using Microsoft.Extensions.Logging;
using ShopFood.Domain.Interfaces.Logger;

namespace ShopFood.Domain.Utils.Logger
{
    /// <summary>
    /// Class to logger information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="logger"></param>
    public class ShopFoodLogger<T>(ILogger<T> logger) : IShopFoodLogger<T> where T : class
    {
        private readonly ILogger<T> _logger = logger;


        #region Public

        /// <summary>
        /// Set Error Log
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public Task<bool> SetErrorLog(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Set Information Log
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<bool> SetInfoLog(string message)
        {
            _logger.LogInformation(message);
            return Task.FromResult(true);
        }

        #endregion Public
    }
}
