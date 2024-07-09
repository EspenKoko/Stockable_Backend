//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Stockable_Backend.Model
//{
//    public class ErrorLogErrorCodes
//    {
//        [Key, Column(Order = 1)]
//        [ForeignKey("ErrorLog")]
//        public int errorLogId { get; set; }
//        public ErrorLog errorLog { get; set; }

//        [Key, Column(Order = 2)]
//        [ForeignKey("ErrorCode")]
//        public int errorCodeId { get; set; }
//        public ErrorCode errorCode { get; set; }

//        [ForeignKey("Printer")]
//        public int printerId { get; set; }
//        public AssignedPrinter printer { get; set; }
//    }

//}
