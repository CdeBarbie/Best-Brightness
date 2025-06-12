using System.ComponentModel.DataAnnotations;

namespace FinalBestBrightnessStore.Models
{
    public class CustomerOrder
    {

        [Key]
        public int Id { get; set; }
        public int SalePersonId { get; set; }
        public DateTime DateOfOrder { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
