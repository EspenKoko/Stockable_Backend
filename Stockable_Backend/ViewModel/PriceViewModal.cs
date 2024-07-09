using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class PriceViewModal
    {
        public int price { get; set; }
        public DateTime priceDate { get; set; }
        public int stockId { get; set; }
    }
}
