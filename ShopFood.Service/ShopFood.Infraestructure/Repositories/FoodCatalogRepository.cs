using Microsoft.Extensions.Configuration;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Repository;
using SP = ShopFood.Domain.Variables.StoreProcedures;

namespace ShopFood.Infraestructure.Repositories
{
    /// <summary>
    /// Class to access data to Food Catalog
    /// </summary>
    public class FoodCatalogRepository : DbContext, IFoodCatalogRepository
    {

        #region Ctor
        /// <summary>
        /// Ctor to FoodCatalogRepository
        /// </summary>
        /// <param name="configuration">Parameter to get information from app settings type of Microsoft.Extensions.Configuration </param>
        public FoodCatalogRepository(IConfiguration configuration) : base(configuration) { } 
        #endregion

        #region Public Methods

        /// <summary>
        /// Get a food catalog by id
        /// </summary>
        /// <param name="id">Parameter to identify the food in the catalog</param>
        /// <returns>Food Catalog result</returns>
        public async Task<FoodCatalog> GetByIdAsync(Guid id) => await GetAsyncFirst<FoodCatalog>($"{SP.EXEC} {SP.FoodCatalog_GetBy_Id} '{id}'");

        /// <summary>
        /// Method to get all Food Catalog
        /// </summary>
        /// <returns>List of FoodCatalog</returns>
        public async Task<IEnumerable<FoodCatalog>> GetAllAsync() => await ExecuteQuery<FoodCatalog>($"{SP.EXEC} {SP.FoodCatalog_GetAll}");

        /// <summary>
        /// Method to create a Food Catalog
        /// </summary>
        /// <param name="entity">Parameter with Food Catalog data info</param>
        public async Task InsertAsync(FoodCatalog entity)
        {
            var parameters = new
            {
                Name = entity.Name,
                Code = entity.Code,
                Description = entity.Description,
                Quantity = entity.Stock,
                Price = entity.Price,
                UserCreatedId = entity.UserCreatedId
            };

            await ExecuteCommand(SP.FoodCatalog_Insert, parameters);
        }

        /// <summary>
        /// Method to update a Food Catalog
        /// </summary>
        /// <param name="entity">Parameter with Food Catalog data info</param>
        public async Task UpdateAsync(FoodCatalog entity)
        {
            var parameters = new
            {
                IdFoodCatalog = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                Description = entity.Description,
                Active = entity.Active,
                Price = entity.Price,
                Quantity = entity.Stock,
                UserCreatedId = entity.UserCreatedId
            };
            await ExecuteCommand(SP.FoodCatalog_Update, parameters);
        }

        /// <summary>
        /// Method to delete a Food Catalog
        /// </summary>
        /// <param name="id">Parameter to identify the food in the catalog</param>
        public async Task DeleteAsync(Guid id) => await ExecuteCommand($"{SP.EXEC} {SP.FoodCatalog_Delete} '{id}'"); 
        #endregion
    }
}
