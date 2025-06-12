namespace FinalBestBrightnessStore.Models
{
    public class SaleSlipViewModel
    {
        public CustomerOrder Order { get; set; }
        public string SalesPersonName { get; set; }
        public int? SalesPersonId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
