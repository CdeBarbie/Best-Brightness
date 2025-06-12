namespace FinalBestBrightnessStore.Models
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public string SalesPersonName { get; set; }
        public DateTime DateOfOrder { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class ReportOrder
    {
        public int SalePersonId { get; set; }
        public string SalesPersonName { get; set; }
        public int NumberOfProductsSold { get; set; }
        public DateTime DateOfOrder { get; set; }
        public decimal TotalAmountMade { get; set; }
    }
}
