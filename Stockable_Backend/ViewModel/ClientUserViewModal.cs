using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class ClientUserViewModal
    {
        public string? clientUserPosition { get; set; }
        public int clientId { get; set; }
        public string userId { get; set; }
        public int branchId { get; set; }

    }
}
