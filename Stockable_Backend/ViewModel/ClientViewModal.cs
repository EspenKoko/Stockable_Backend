using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class ClientViewModal
    {
        public string? clientName { get; set; }
        public string? clientNumber { get; set; }
        public string? clientEmail { get; set; }
        public string? clientAddress { get; set; }

    }
}
