using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IEmployeeTypeRepository
    {
        //EmployeeType
        Task<EmployeeType[]> GetAllEmployeeTypesAsync();
        Task<EmployeeType> GetEmployeeTypeAsync(int employeeTypeId);
        Task<int> AddEmployeeTypeAsync(EmployeeTypeViewModal employeeType);
        Task<int> UpdateEmployeeTypeAsync(int employeeTypeId, EmployeeTypeViewModal employeeType);
        Task<int> DeleteEmployeeTypeAsync(int employeeTypeId);
        Task<List<EmployeeType>> SearchEmployeeTypeAsync(string searchString);
        Task<IEnumerable<EmployeeType>> GetAllEmployeeTypesFromStoredProcedureAsync();

    }
}
 