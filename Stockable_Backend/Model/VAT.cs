using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class VAT
    {
        [Key]
        public int vatId { get; set; }
        public decimal vatPercent { get; set;}
        public DateTime vatDate { get; set; }


    }
}
