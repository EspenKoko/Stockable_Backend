using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class Repair
    {
        [Key]
        public int repairId { get; set; }

        [ForeignKey("ErrorLog")]
        public int errorLogId { get; set; }
        public ErrorLog errorLog { get; set; }

        [ForeignKey("RepairStatus")]
        public int repairStatusId { get; set; }
        public RepairStatus repairStatus { get; set; }

        [ForeignKey("Employee")]
        public int employeeId { get; set; }
        public Employee employee { get; set; }
    }
}
