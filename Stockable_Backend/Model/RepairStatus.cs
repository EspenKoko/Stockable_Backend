using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class RepairStatus
    {
        [Key]
        public int repairStatusId { get; set; }
        public string repairStatusName { get; set; }
    }
}
