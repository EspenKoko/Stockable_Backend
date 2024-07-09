using System.ComponentModel.DataAnnotations;
using Stockable_Backend.Model;

namespace Stockable_Backend.Model
{
    public class Province
    {
        [Key]
        public int provinceId { get; set; }
        public string? provinceName { get; set; }
    }
}
