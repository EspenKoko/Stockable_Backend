using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class ClientInvoice
    {
        [Key]
        public int clientInvoiceId { get; set; }
        public string clientInvoiceNumber { get; set; }
        public DateTime clientInvoiceDate { get; set; }
    }
}
