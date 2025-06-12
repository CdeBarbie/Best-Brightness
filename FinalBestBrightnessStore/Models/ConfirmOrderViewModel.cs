namespace FinalBestBrightnessStore.Models
{
    public class ConfirmOrderViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; }
        public decimal CashAmount { get; set; }
    }
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
