namespace ShopFood.Domain.DTOs.Requests
{
    public class FoodOrderRequest
    {
        public List<FoodOrderDetailRequest> FoodCatalogList { get; set; }
        public Guid UserCreatedId { get; set; }
    }

    public class FoodOrderDetailRequest
    {
        public Guid FoodCatalogId { get; set; }
        public int Quantity { get; set; }
    }
}
