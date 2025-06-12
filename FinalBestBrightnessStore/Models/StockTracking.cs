using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalBestBrightnessStore.Models
{
    public class StockTracking
    {
        [Key]
        public int stockTrackId { get; set; }

        public int stockManId { get; set; }
        public int prodId { get; set; }

        public string dateOfstock { get; set; }
    }
}
