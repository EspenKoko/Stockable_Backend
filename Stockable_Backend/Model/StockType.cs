using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class StockType
    {
        [Key]
        [Required]
        public int stockTypeId { get; set; }
        public string stockTypeName { get; set; }

        [ForeignKey("StockCategory")]
        [Required]
        public int stockCategoryId { get; set; }
        public StockCategory? stockCategory { get; set; }
    }
}
