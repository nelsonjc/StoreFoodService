namespace ShopFood.Domain.Interfaces.Logger
{
    public interface IShopFoodLogger<T> where T : class
    {
        Task<bool> SetErrorLog(Exception ex);
        Task<bool> SetInfoLog(string message);
    }
}
