using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class Stock
    {
        [Key]
        public int stockId { get; set; }
        public string stockName { get; set; }
        public string stockDescription { get; set;}
        public int qtyOnHand { get; set; }
        public int minStockThreshold { get; set; }
        public int maxStockThreshold { get; set; }
        //public string image { get; set; }

        [ForeignKey("StockType")]
        [Required]
        public int stockTypeId { get; set; }
        public StockType? stockType { get; set; }
    }
}
