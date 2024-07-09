using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class ClientOrder
    {
        [Key]
        public int clientOrderId { get; set; }
        //public int qty { get; set; }

        [ForeignKey("ClientOrderStatus")]
        public int clientOrderStatusId { get; set; }
        public ClientOrderStatus clientOrderStatus{ get; set; }

        [ForeignKey("PaymentType")]
        public int paymentTypeId { get; set; }
        public PaymentType paymentType{ get; set; }

        [ForeignKey("ClientInvoice")]
        public int clientInvoiceId { get; set; }
        public ClientInvoice clientInvoice{ get; set; }

        [ForeignKey("ClientUser")]
        public int clientUserId { get; set; }
        public ClientUser clientUser { get; set; }

        //[ForeignKey("Stock")]
        //public int stockId { get; set; }
        //public Stock stock { get; set; }


    }
}
