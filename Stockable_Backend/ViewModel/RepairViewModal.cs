using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class RepairViewModal
    {
        public int errorLogId { get; set; }
        public int repairStatusId { get; set; }
        public int employeeId { get; set; }
    }
}
