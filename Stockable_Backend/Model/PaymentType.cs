using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class PaymentType
    {
        [Key]
        public int paymentTypeId { get; set; }
        public string paymentTypeName { get; set; }
    }
}
