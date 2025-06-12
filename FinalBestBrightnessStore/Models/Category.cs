using System.ComponentModel.DataAnnotations;

namespace FinalBestBrightnessStore.Models
{
    public class Category
    {
        [Key]
        public int catId { get; set; }
        public string catName { get; set; }
    }
}
