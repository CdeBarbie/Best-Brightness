using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalBestBrightnessStore.Models
{
    public class SalesTracking
    {
        /*[Key]
        public int salesTrackId { get; set; }
        public int salePersonId { get; set; }
        public int prodId { get; set; }
        public string dateOfSale { get; set; }*/
        [Key]
        public int salesTrackId { get; set; }
        public int salePersonId { get; set; }
        public DateTime dateOfSale { get; set; }

        public ICollection<SalesTrackingProduct> ProductsSold { get; set; }
    }
}
