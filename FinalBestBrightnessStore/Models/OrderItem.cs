using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBestBrightnessStore.Models
{
    public class OrderItem
    {
        [Key]
        public int ItemId { get; set; }
        public int prodId { get; set; }
        [ForeignKey("prodId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        [ForeignKey("Id")]
        public CustomerOrder CustomerOrder { get; set; }

        // Make the ProductName property mutable
        [NotMapped]
        public string ProductName { get; set; }

    }
}
