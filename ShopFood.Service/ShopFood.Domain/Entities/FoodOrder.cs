namespace ShopFood.Domain.Entities
{
    public class FoodOrderHead
    {
        public Guid Id { get; set; }        
        public int FoodQuantity { get; set; }
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusCode { get; set; }
        public Guid UserCreatedId { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public List<FoodOrderDetail> Details { get; set; }
    }

    public class FoodOrderDetail
    {
        public int ItemNumber { get; set; }
        public Guid Id { get; set; }
        public Guid FoodCatalogId { get; set; }
        public string FoodCatalogName { get; set; }
        public string FoodCatalogDescription { get; set; }
        public int Quantity  { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public bool Active { get; set; }        
    }
}
