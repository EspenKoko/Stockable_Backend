using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stockable_Backend.Model;


namespace Stockable_Backend.Model
{
    public class Employee
    {
        [Key]
        [Required]
        public int employeeId { get; set; }
        public DateTime empHireDate { get; set; }

        [ForeignKey("EmployeeType")]
        [Required]

        public int employeeTypeId { get; set; }
        public EmployeeType? employeeType { get; set; }

        [ForeignKey("User")]
        [Required]
        public string userId { get; set; }
        public User? user { get; set; }

    }
}
