using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class Markup
    {
        [Key]
        public int markupId { get; set; }
        public decimal markupPercent { get; set; }
        public DateTime markupDate { get; set; }
    }
}
