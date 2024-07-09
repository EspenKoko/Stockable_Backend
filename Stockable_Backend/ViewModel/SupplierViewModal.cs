using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class SupplierViewModal
    {
        public string supplierName { get; set; }
        public string supplierAddress { get; set; }
        public string supplierContactNumber { get; set; }
        public string supplierEmail { get; set; }

    }
}
