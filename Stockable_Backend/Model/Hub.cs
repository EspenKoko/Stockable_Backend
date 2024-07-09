using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class Hub
    {
        [Key]
        [Required]
        public int hubId { get; set; }
        public string? hubName { get; set; }
        public int qtyOnHand { get; set; }
        public int hubPrinterThreshold { get; set; }


        [ForeignKey("City")]
        [Required]
        public int cityId { get; set; }
        public City? city { get; set; }
    }
}
