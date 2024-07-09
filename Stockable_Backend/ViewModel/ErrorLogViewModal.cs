namespace Stockable_Backend.ViewModel
{
    public class ErrorLogViewModal
    {
        public DateTime errorlogDate { get; set; }
        public string errorLogDescription { get; set; }
        public int errorLogStatusId { get; set; }
        public int clientUserId { get; set; }
        public int assignedPrinterId { get; set; }
        public int errorCodeId { get; set; }
        //public int assignedTechnicianId { get; set; }
    }
}
