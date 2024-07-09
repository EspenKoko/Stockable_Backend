using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class ClientOrderViewModal
    {
        public int clientOrderStatusId { get; set; }
        public int paymentTypeId { get; set; }
        public int clientInvoiceId { get; set; }
        public int clientUserId { get; set; }
        //public int stockId { get; set; }
        //public int qty { get; set; }
    }
}
