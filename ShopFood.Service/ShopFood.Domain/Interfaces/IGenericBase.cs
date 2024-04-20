namespace ShopFood.Domain.Interfaces
{
    public interface IGenericBase<T, T2>
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T?>> GetAllAsync();
        Task InsertAsync(T2 entity);
        Task UpdateAsync(T2 entity);
        Task DeleteAsync(Guid id);
    }
}
