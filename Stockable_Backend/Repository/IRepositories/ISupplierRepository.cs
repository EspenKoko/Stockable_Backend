using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface ISupplierRepository
    {
        //Supplier
        Task<Supplier> GetSupplierAsync(int supplierId);
        Task<Supplier[]> GetAllSuppliersAsync();
        Task<int> AddSupplierAsync(SupplierViewModal supplier);
        Task<int> UpdateSupplierAsync(int supplierId, SupplierViewModal supplier);
        Task<int> DeleteSupplierAsync(int supplierId);
        Task<List<Supplier>> SearchSupplierAsync(string searchString);
    }
}
