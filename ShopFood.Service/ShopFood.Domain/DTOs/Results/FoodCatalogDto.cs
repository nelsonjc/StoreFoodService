namespace ShopFood.Domain.DTOs.Results
{
    public class FoodCatalogDto
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

    public class FoodCatalogCustomerDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
