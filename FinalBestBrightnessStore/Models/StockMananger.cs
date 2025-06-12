using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalBestBrightnessStore.Models
{
    public class StockMananger
    {
        [Key]
        public int stockManId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string dateOfbirth { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }

    }
}
