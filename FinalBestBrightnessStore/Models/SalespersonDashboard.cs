namespace FinalBestBrightnessStore.Models
{
    public class SalespersonDashboard
    {
        public List<Product> Products { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
public class CartItem
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}