using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class StockSupplierOrder
    {
        [Key]
        [ForeignKey("Stock")]
        public int stockId { get; set; }
        public Stock stock { get; set; }

        [Key]
        [ForeignKey("SupplierOrder")]
        public int supplierOrderId { get; set; }
        public SupplierOrder supplierOrder { get; set; }

        public int qty { get; set; }

    }
}
