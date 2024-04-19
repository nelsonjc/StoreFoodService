using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Repository;
using System.Text.Json;

namespace ShopFood.Infraestructure.Repositories
{
    public class FoodOrderRepository : DbContext, IFoodOrderRepository
    {
        public FoodOrderRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<FoodOrderHead> InsertAsync(FoodOrderRequest request)
        {
            using var conn = new SqlConnection(_connectionString);

            string foodCatalogListJson = JsonSerializer.Serialize(request.FoodCatalogList);
            var parameters = new {
                FoodCatalogListJson = foodCatalogListJson,
                UserCreatedId  = request.UserCreatedId
            };

            var resultCommand = await conn.QueryMultipleAsync("[dbo].[SP_FoodCatalogOrder_Insert]", parameters);
            var result = resultCommand.Read<FoodOrderHead>().FirstOrDefault();

            if (result != null)
                result.FoodOrderDetails = resultCommand.Read<FoodOrderDetail>().ToList();

            await conn.CloseAsync();
            await conn.DisposeAsync();
            return result;
        }
    }
}
