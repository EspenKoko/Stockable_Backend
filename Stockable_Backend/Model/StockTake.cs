using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class StockTake
    {
        [Key]
        public int stockTakeId { get; set; }

        public DateTime stockTakeDate { get; set; }


        [ForeignKey("Employee")]
        public int employeeId { get; set; }
        public Employee employee { get; set; }


    }
}
