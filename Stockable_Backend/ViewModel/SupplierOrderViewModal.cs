using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class SupplierOrderViewModal
    {
        public int employeeId { get; set; }
        public int supplierOrderStatusId { get; set; }
        public int supplierId { get; set; }
        public DateTime date { get; set; }

    }
}
