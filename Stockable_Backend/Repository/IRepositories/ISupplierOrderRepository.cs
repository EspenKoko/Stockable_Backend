using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface ISupplierOrderRepository
    {
        Task<SupplierOrder[]> GetAllSupplierOrdersAsync();
        Task<SupplierOrder> GetSupplierOrderAsync(int supplierOrderId);
        Task<int> AddSupplierOrderAsync(SupplierOrderViewModal supplierOrder);
        Task<int> UpdateSupplierOrderAsync(int supplierOrderId, SupplierOrderViewModal supplierOrder);
        Task<int> DeleteSupplierOrderAsync(int supplierOrderId);
        Task<List<SupplierOrder>> SearchSupplierOrderAsync(string searchString);
    }

}
