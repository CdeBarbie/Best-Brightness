namespace FinalBestBrightnessStore.Models
{
    public class AdminDashboard
    {
        public int ProductCount { get; set; }
        public decimal TotalIncome { get; set; }
        public int SalesPersonCount { get; set; }
        public int StockManagerCount { get; set; }

        public int CategoryCount { get; set; }
        public int ProductsInCategoryCount { get; set; }
        public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; }
    }
}
