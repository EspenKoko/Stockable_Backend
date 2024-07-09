using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class ErrorLogStatus
    {
        [Key]
        public int errorLogStatusId { get; set; }
        public string errorLogStatusName { get; set; }
    }
}
