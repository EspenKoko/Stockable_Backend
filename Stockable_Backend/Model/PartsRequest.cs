using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class PartsRequest
    {
        public int qty { get; set; }

        [Key]
        [ForeignKey("Stock")]
        public int stockId { get; set; }
        public Stock stock { get; set; }

        [Key]
        [ForeignKey("PurchaseOrder")]
        public int purchaseOrderId { get; set; }
        public PurchaseOrder purchaseOrder { get; set; }
    }
}
