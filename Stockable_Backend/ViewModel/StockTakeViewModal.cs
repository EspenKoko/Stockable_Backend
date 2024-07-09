using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class StockTakeViewModal
    {
        public DateTime stockTakeDate { get; set; }
        public int employeeId { get; set; }
    }
}
