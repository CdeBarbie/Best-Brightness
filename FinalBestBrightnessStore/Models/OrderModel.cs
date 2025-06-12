namespace FinalBestBrightnessStore.Models
{
    public class OrderModel
    {
        public string productName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    public class ConfirmOrderModel
    {
        public List<OrderModel> Orders { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
