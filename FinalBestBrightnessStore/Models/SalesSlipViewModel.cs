namespace FinalBestBrightnessStore.Models
{
    public class SalesSlipViewModel
    {
        public int OrderId { get; set; }
        public DateTime DateOfSale { get; set; }
        public string SalesPersonName { get; set; }
        public int SalesPersonId { get; set; }
        public CustomerOrder Order { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
