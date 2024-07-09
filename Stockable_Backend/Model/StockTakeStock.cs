using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class StockTakeStock
    {
        public int qty { get; set; }

        [Key]
        [ForeignKey("Stock")]
        public int stockId { get; set; }
        public Stock stock { get; set; }

        [Key]
        [ForeignKey("StockTake")]
        public int stockTakeId { get; set; }
        public StockTake stockTake { get; set; }
    }
}
