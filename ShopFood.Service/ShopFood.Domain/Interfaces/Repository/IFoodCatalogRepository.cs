using ShopFood.Domain.Entities;

namespace ShopFood.Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface to food catalog to access data info
    /// </summary>
    public interface IFoodCatalogRepository : IGenericBase<FoodCatalog, FoodCatalog>
    {
    }
}
