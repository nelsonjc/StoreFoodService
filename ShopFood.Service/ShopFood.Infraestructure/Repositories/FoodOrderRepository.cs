using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Repository;
using System.Text.Json;
using SP = ShopFood.Domain.Variables.StoreProcedures;

namespace ShopFood.Infraestructure.Repositories
{
    /// <summary>
    /// Class to access data to Food Order
    /// </summary>
    public class FoodOrderRepository : DbContext, IFoodOrderRepository
    {
        #region Ctor
        /// <summary>
        /// Ctor to FoodOrderRepository
        /// </summary>
        /// <param name="configuration">Parameter to get information from app settings type of Microsoft.Extensions.Configuration </param>
        public FoodOrderRepository(IConfiguration configuration) : base(configuration) { }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to confirm a food order
        /// </summary>
        /// <param name="id">Parameter type of Guid with Id of Food Order Head</param>
        public async Task ConfirmAsync(Guid id)
        {
            await this.ExecuteCommand($"{SP.EXEC} {SP.FoodOrder_Confirm} '{id}'");
        }

        /// <summary>
        /// Method to create a Food Order with multiple food details
        /// </summary>
        /// <param name="request">Parameter with request date of food order type of FoodOrderRequest</param>
        /// <returns> Food Order Head Result</returns>
        public async Task<FoodOrderHead> InsertAsync(FoodOrderRequest request)
        {
            using var conn = new SqlConnection(_connectionString);

            string foodCatalogListJson = JsonSerializer.Serialize(request.FoodCatalogList);
            var parameters = new
            {
                FoodCatalogListJson = foodCatalogListJson,
                UserCreatedId = request.UserCreatedId
            };

            var resultCommand = await conn.QueryMultipleAsync(SP.FoodOrder_Insert, parameters);
            var result = resultCommand.Read<FoodOrderHead>().FirstOrDefault();

            if (result != null)
                result.FoodOrderDetails = resultCommand.Read<FoodOrderDetail>().ToList();

            await conn.CloseAsync();
            await conn.DisposeAsync();
            return result;
        } 
        #endregion
    }
}
