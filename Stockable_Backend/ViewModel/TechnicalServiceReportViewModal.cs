using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class TechnicalServiceReportViewModal
    {
        public int purchaseOrderId { get; set; }
        public string repairsDone { get; set; }
        //public int timeElapst { get; set; }
        //public DateTime TSRDate { get; set; }
    }
}
