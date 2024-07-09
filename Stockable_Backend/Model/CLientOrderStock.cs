using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class ClientOrderStock
    {
        [Key]
        [ForeignKey("Stock")]
        public int stockId { get; set; }
        public Stock stock { get; set; }

        [Key]
        [ForeignKey("ClientOrder")]
        public int clientOrderId { get; set; }
        public ClientOrder clientOrder { get; set; }

        public int qty { get; set; }
    }
}
