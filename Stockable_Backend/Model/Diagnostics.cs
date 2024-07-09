using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class Diagnostics
    {
        [Key]
        public int diagnosticsId { get; set; }
        public string diagnosticComment { get; set; }
        public bool rollerCheck { get; set; }
        public bool lcdScreenCheck { get; set; }
        public bool powerSupplyCheck { get; set; }
        public bool motherboardCheck { get; set; }
        public bool hopperCheck { get; set; }
        public bool beltCheck { get; set; }
        public bool ethernetPortCheck { get; set; }

        [ForeignKey("Repair")]
        public int repairId { get; set; }
        public Repair repair { get; set; }
    }
}
