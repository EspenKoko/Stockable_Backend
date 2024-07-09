using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class Price
    {
        [Key]
        public int priceId { get; set; }
        public int price { get; set; }
        public DateTime priceDate { get; set; }

        [ForeignKey("Stock")]
        [Required]
        public int stockId { get; set; }
        public Stock? stock { get; set; }

    }
}
