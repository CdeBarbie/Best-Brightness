using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalBestBrightnessStore.Models
{
    public class Product
    {
        [Key]
        public int prodId { get; set; }
        public string productName { get; set; }
        public double price { get; set; }
        public int quantinty { get; set; }
        public string expDate { get; set; }
        public string Category { get; set; }

    }
}
