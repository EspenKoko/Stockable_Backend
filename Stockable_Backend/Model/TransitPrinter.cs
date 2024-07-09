using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class TransitPrinter
    {
        [Key]
        public int transitPrinterId { get; set; }
        public string technicianId { get; set; }
        public int assignedPrinterId { get; set; }
        public int errorLogId { get; set; }
        public DateTime date { get; set; }

    }
}
