using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class TechnicalServiceReport
    {
        [Key]
        public int technicalServiceReportId{ get; set; }

        [ForeignKey("PurchaseOrder")]
        public int purchaseOrderId { get; set; }
        public PurchaseOrder purchaseOrder { get; set; }

        public string repairsDone { get; set; }
        //public int timeElapst { get; set; }
        //public DateTime TSRDate { get; set; }
    }
}
