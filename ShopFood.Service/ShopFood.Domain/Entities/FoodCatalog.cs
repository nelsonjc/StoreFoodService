namespace ShopFood.Domain.Entities
{
    public class FoodCatalog
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public Guid UserCreatedId { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public int Stock { get; set; }
    }
}
