using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class AssignedTechnician
    {
        [Key, Column(Order = 1)]
        [ForeignKey("ErrorLog")]
        public int errorLogId { get; set; }
        public ErrorLog errorLog { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Employee")]
        public int employeeId { get; set; }
        public Employee employee { get; set; }

        //public bool isAssigned { get; set; }
    }
}
