using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces;
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
        /// TODO: Method pending to implement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();

        /// <summary>
        /// Method to get all food order
        /// </summary>
        /// <returns>A List of FoodOrder Head</returns>
        public async Task<IEnumerable<FoodOrderHead?>> GetAllAsync() => await ExecuteQuery<FoodOrderHead>($"{SP.EXEC} {SP.SP_FoodOrder_GetAll}");

        /// <summary>
        /// Method to get a food order by id
        /// </summary>
        /// <param name="id">Parameter to identify the fodd order to get</param>
        /// <returns>A FoodOrder Head with details</returns>
        public async Task<FoodOrderHead?> GetByIdAsync(Guid id)
        {
            using var conn = new SqlConnection(_connectionString);

            var parameters = new { FoodOrderId = id };

            var resultCommand = await conn.QueryMultipleAsync(SP.SP_FoodOrder_GetBy_Id, parameters);
            var result = resultCommand.Read<FoodOrderHead>().FirstOrDefault();

            if (result != null)
                result.Details = resultCommand.Read<FoodOrderDetail>().ToList();

            await conn.CloseAsync();
            await conn.DisposeAsync();

            return result;
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
                result.Details = resultCommand.Read<FoodOrderDetail>().ToList();

            await conn.CloseAsync();
            await conn.DisposeAsync();
            return result;
        }

        /// <summary>
        /// TODO: Method pending to implement
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateAsync(FoodOrderRequest entity) => throw new NotImplementedException();

        /// <summary>
        /// TODO: Method pending to implement
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task IGenericBase<FoodOrderHead, FoodOrderRequest>.InsertAsync(FoodOrderRequest entity) => throw new NotImplementedException();
        
        #endregion
    }
}
