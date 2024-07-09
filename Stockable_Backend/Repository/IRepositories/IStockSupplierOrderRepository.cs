using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IStockSupplierOrderRepository
    {
        //StockSupplierOrder
        Task<StockSupplierOrder[]> GetAllStockSupplierOrderAsync();
        Task<StockSupplierOrder> GetStockSupplierOrderAsync(int supplierOrderId);
        Task<int> AddStockSupplierOrderAsync(StockSupplierOrderViewModal stockSupplierOrder);
        Task<int> UpdateStockSupplierOrderAsync(int supplierOrderId, StockSupplierOrderViewModal stockSupplierOrder);
        Task<int> DeleteStockSupplierOrderAsync(int supplierOrderId);
        Task<int> DeleteStockSupplierOrderItemAsync(int supplierOrderId, int stockId);
        Task<List<StockSupplierOrder>> SearchStockSupplierOrderAsync(string searchString);
    }
}
