using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class AssignedPrinter
    {
        [Key]
        public int assignedPrinterId { get; set; }
        public string serialNumber { get; set; }
        public string printerModel { get; set; }


        [ForeignKey("Client")]
        public int clientId { get; set; }
        public Client client { get; set; }

        //[ForeignKey("PrinterModel")]
        //public int printerModelId { get; set; }
        //public PrinterModel printerModel { get; set; }

        [ForeignKey("PrinterStatus")]
        public int printerStatusId { get; set; }
        public PrinterStatus printerStatus { get; set; }

    }
}
