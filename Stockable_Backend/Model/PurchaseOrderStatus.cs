using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class PurchaseOrderStatus
    {
        [Key]
        public int purchaseOrderStatusId { get; set; }
        public string purchaseOrderStatusName { get; set; }
    }
}
