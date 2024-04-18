using Microsoft.Extensions.Logging;
using ShopFood.Domain.Interfaces.Logger;

namespace ShopFood.Domain.Utils.Logger
{
    public class ShopFoodLogger<T> : IShopFoodLogger<T> where T : class
    {
        #region Properties

        private readonly ILogger<T> _logger;

        #endregion Properties


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public ShopFoodLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        #endregion Constructor


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
