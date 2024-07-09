namespace Stockable_Backend.ViewModel
{
    public class DiagnosticsViewModal
    {
        public string diagnosticComment { get; set; }
        public bool rollerCheck { get; set; }
        public bool lcdScreenCheck { get; set; }
        public bool powerSupplyCheck { get; set; }
        public bool motherboardCheck { get; set; }
        public bool hopperCheck { get; set; }
        public bool beltCheck { get; set; }
        public bool ethernetPortCheck { get; set; }
        public int repairId { get; set; }

    }
}
