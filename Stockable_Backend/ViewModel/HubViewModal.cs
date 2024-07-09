using Stockable_Backend.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class HubViewModal
    {
        
        public string? hubName { get; set; }
        public int qtyOnHand { get; set; }
        public int cityId { get; set; }

    }
}
