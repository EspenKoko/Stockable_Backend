using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class RepairDiagnostic
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Repair")]
        public int repairId { get; set; }
        public Repair repair { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Diagnostics")]
        public int diagnosticsId { get; set; }
        public Diagnostics diagnostics{ get; set; }

        public bool isComplete { get; set; }
    }
}
