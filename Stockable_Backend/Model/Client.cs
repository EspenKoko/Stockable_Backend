using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class Client
    {
        [Key]
        [Required]
        public int clientId { get; set; }
        public string? clientName { get; set; }
        public string? clientNumber { get; set; }
        public string? clientEmail { get; set; }
        public string? clientAddress { get; set; }

    }
}
