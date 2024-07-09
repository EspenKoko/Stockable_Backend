using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IPurchaseOrderRepository
    {
        //PurchaseOrder
        Task<PurchaseOrder[]> GetAllPurchaseOrderAsync();
        Task<PurchaseOrder> GetPurchaseOrderAsync(int purchaseOrderId);
        Task<int> AddPurchaseOrderAsync(PurchaseOrderViewModal purchaseOrder);
        Task<int> UpdatePurchaseOrderAsync(int purchaseOrderId, PurchaseOrderViewModal purchaseOrder);
        Task<int> DeletePurchaseOrderAsync(int purchaseOrderId);
        Task<List<PurchaseOrder>> SearchPurchaseOrderAsync(string searchString);
    }
}
