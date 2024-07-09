using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class PrinterStatus
    {
        [Key]
        public int printerStatusId { get; set; }
        public string printerStatusName { get; set;}
    }
}
