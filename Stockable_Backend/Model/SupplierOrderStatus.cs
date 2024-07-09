using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class SupplierOrderStatus
    {
        [Key]
        public int supplierOrderStatusId { get; set; }
        public string supplierOrderStatusName { get; set; }
    }
}
