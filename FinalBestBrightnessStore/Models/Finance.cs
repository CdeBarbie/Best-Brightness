using System.ComponentModel.DataAnnotations;

namespace FinalBestBrightnessStore.Models
{
    public class Finance
    {
        [Key]
        public int Id { get; set; }
        public DateTime dateOfSale {  get; set; }
        public int salePersonId { get; set; }
        public decimal amount { get; set; }

    }
}
