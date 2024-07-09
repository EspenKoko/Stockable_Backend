using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class AuditTrail
    {
        [Key]
        public int auditTrailId { get; set; }
        public DateTime date { get; set; }
        public string userId{ get; set; }
        public string userName{ get; set; }
        public string userAction { get; set; }
    }
}
