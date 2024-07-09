using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class PurchaseOrder
    {
        [Key]
        public int purchaseOrderId { get; set; }
        public DateTime purchaseOrderDate { get; set; }
        public int repairTime { get; set; }


        [ForeignKey("Repair")]
        public int repairId { get; set; }
        public Repair repair { get; set; }

        [ForeignKey("PurchaseOrderStatus")]
        public int purchaseOrderStatusId { get; set; }
        public PurchaseOrderStatus purchaseOrderStatus { get; set; }
    }
}
