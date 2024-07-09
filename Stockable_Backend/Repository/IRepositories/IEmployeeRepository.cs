using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IEmployeeRepository
    {
        //Employee
        Task<Employee[]> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int employeeId);
        Task<int> AddEmployeeAsync(EmployeeViewModal employee);
        Task<int> UpdateEmployeeAsync(int employeeId, EmployeeViewModal employee);
        Task<int> DeleteEmployeeAsync(int employeeId);
        Task<List<Employee>> SearchEmployeeAsync(string searchString);
    }
}
