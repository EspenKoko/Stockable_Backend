using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface ISupplierOrderStatusRepository
    {
        //SupplierOrderStatus
        Task<SupplierOrderStatus[]> GetAllSupplierOrderStatusAsync();
        Task<SupplierOrderStatus> GetSupplierOrderStatusAsync(int supplierOrderStatusId);
        Task<int> AddSupplierOrderStatusAsync(SupplierOrderStatusViewModal supplierOrderStatus);
        Task<int> UpdateSupplierOrderStatusAsync(int supplierOrderStatusId, SupplierOrderStatusViewModal supplierOrderStatus);
        Task<int> DeleteSupplierOrderStatusAsync(int supplierOrderStatusId);
        Task<List<SupplierOrderStatus>> SearchSupplierOrderStatusAsync(string searchString);
    }
}
