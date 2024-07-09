using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class ErrorLog
    {
        [Key]
        public int errorLogId { get; set; }
        public DateTime errorLogDate { get; set; }
        public string errorLogDescription { get; set; }

        [ForeignKey("ErrorLogStatus")]
        public int errorLogStatusId { get; set; }
        public ErrorLogStatus errorLogStatus { get; set; }

        [ForeignKey("ClientUser")]
        public int clientUserId { get; set; }
        public ClientUser clientUser { get; set; }

        [ForeignKey("AssignedPrinter")]
        public int assignedPrinterId { get; set; }
        public AssignedPrinter assignedPrinter { get; set; }

        [ForeignKey("ErrorCode")]
        public int errorCodeId { get; set; }
        public ErrorCode errorCode { get; set; }

        //[ForeignKey("AssignedTechnician")]
        //public int assignedTechnicianId { get; set; }
        //public AssignedTechnician assignedTechnician { get; set; }


    }
}
