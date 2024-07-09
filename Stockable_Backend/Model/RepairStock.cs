using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class RepairStock
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Stock")]
        public int stockId { get; set; }
        public Stock stock { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Repair")]
        public int repairId { get; set; }
        public Repair repair { get; set; }

        [Key, Column(Order = 3)]
        [ForeignKey("PurchaseOrder")]
        public int purchaseOrderId { get; set; }
        public PurchaseOrder purchaseOrder { get; set; }

        public int qty{ get; set; }
    }
}
