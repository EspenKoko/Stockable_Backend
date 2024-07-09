using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IPurchaseOrderStatusRepository
    {
        //PurchaseOrderStatus
        Task<PurchaseOrderStatus[]> GetAllPurchaseOrderStatusAsync();
        Task<PurchaseOrderStatus> GetPurchaseOrderStatusAsync(int purchaseOrderStatusId);
        Task<int> AddPurchaseOrderStatusAsync(PurchaseOrderStatusViewModal purchaseOrderStatus);
        Task<int> UpdatePurchaseOrderStatusAsync(int purchaseOrderStatusId, PurchaseOrderStatusViewModal purchaseOrderStatus);
        Task<int> DeletePurchaseOrderStatusAsync(int purchaseOrderStatusId);
        Task<List<PurchaseOrderStatus>> SearchPurchaseOrderStatusAsync(string searchString);
    }
}
