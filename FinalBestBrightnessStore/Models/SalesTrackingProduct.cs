using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FinalBestBrightnessStore.Models
{
    public class SalesTrackingProduct
    {
        [Key]
        public int Id { get; set; }
        public int salesTrackId { get; set; }
        [ForeignKey("salesTrackId")]
        public SalesTracking SalesTracking { get; set; }

        public int prodId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Add this line for Price
    }
}
