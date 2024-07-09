using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class SupplierOrder
    {
        [Key]
        public int supplierOrderId { get; set; }

        public DateTime date { get; set; }

        [ForeignKey("Employee")]
        public int employeeId { get; set; }
        public Employee employee { get; set; }

        [ForeignKey("SupplierOrderStatus")]
        public int supplierOrderStatusId { get; set; }
        public SupplierOrderStatus supplierOrderStatus { get; set; }

        [ForeignKey("Supplier")]
        public int supplierId { get; set; }
        public Supplier supplier { get; set; }
    }
}
