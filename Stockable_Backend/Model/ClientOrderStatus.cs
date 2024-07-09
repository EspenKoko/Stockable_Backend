using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class ClientOrderStatus
    {
        [Key]
        public int clientOrderStatusId { get; set; }
        public string clientOrderStatusName{ get; set; }
    }
}
