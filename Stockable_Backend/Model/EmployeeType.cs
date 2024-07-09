using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class EmployeeType
    {
        [Key]
        [Required]
        public int employeeTypeId { get; set; }
        public string employeeTypeName { get; set; }
        public string? employeeTypeDescription { get; set; }

    }
}
