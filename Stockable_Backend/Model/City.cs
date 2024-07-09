using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class City
    {
        [Key]
        [Required]
        public int cityId { get; set; }
        public string? cityName { get; set; }

        [ForeignKey("Province")]
        [Required]

        public int provinceId { get; set; }
        public Province? province { get; set; }

    }
}
