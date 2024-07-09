using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class EmployeeTypeViewModal
    {
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please enter a valid Employeetype name with letters and spaces only.")]
        public string employeeTypeName { get; set; }
        public string? employeeTypeDescription { get; set; }

    }
}
