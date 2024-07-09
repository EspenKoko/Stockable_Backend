using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class ProvinceViewModal
    {
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please enter a valid Province name with letters and spaces only.")]
        public string? provinceName { get; set; }

    }
}
