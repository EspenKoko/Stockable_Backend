using Stockable_Backend.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class EmployeeViewModal
    {
        public DateTime empHireDate { get; set; }
        public int employeeTypeId { get; set; }
        public string userId { get; set; }

    }
}
