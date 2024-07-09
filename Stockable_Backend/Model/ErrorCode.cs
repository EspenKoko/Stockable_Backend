using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class ErrorCode
    {
        [Key]
        [Required]
        public int errorCodeId { get; set; }
        public string  errorCodeName { get; set; }
        public string errorCodeDescription { get; set; }

    }
}
