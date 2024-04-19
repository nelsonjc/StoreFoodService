using Microsoft.Extensions.Configuration;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Repository;

namespace ShopFood.Infraestructure.Repositories
{
    public class FoodCatalogRepository : DbContext, IFoodCatalogRepository
    {
        public FoodCatalogRepository(IConfiguration configuration) : base(configuration) { }
        
        public async Task<FoodCatalog> GetByIdAsync(Guid id)
        {
            return await this.GetAsyncFirst<FoodCatalog>($"EXEC [dbo].[SP_FoodCatalog_GetBy_Id] '{id}'");
        }

        public async Task<IEnumerable<FoodCatalog>> GetAllAsync()
        {
            return await this.ExecuteQuery<FoodCatalog>($"EXEC [dbo].[SP_FoodCatalog_GetAll]");
        }

        public async Task InsertAsync(FoodCatalog entity)
        {
            var parameters = new { Name = entity.Name, Code = entity.Code, Description = entity.Description, Quantity = entity.Stock, Price = entity.Price, UserCreatedId = entity.UserCreatedId };
            await this.ExecuteCommand("[dbo].[SP_FoodCatalog_Insert]", parameters);
        }

        public async Task UpdateAsync(FoodCatalog entity)
        {
            var parameters = new { 
                IdFoodCatalog = entity.Id, 
                Name = entity.Name,
                Code = entity.Code, 
                Description = entity.Description,
                Active = entity.Active,
                Price = entity.Price, 
                Quantity = entity.Stock,
                UserCreatedId = entity.UserCreatedId
            };
            await this.ExecuteCommand("[dbo].[SP_FoodCatalog_Update]", parameters);
        }

        public async Task DeleteAsync(Guid id)
        {
            await this.ExecuteCommand($"EXEC [dbo].[SP_FoodCatalog_Delete] '{id}'");
        }
    }
}
