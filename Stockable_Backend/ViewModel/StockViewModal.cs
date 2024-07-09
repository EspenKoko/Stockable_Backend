using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class StockViewModal
    {
        public string stockName { get; set; }
        public string stockDescription { get; set; }
        public int qtyOnHand { get; set; }
        public int minStockThreshold { get; set; }
        public int maxStockThreshold { get; set; }
        public int stockTypeId { get; set; }
        //public string image { get; set; }

    }
}
