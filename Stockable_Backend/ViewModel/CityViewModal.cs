using Stockable_Backend.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class CityViewModal
    {
        public string? cityName { get; set; }
        public int provinceId { get; set; }
    }
}
