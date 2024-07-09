using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IRepairOrdersRepository
    {
        //RepairOrder
        Task<RepairOrders> GetRepairOrderAsync(int RepairOrderId);
        Task<RepairOrders[]> GetAllRepairOrdersAsync();
        Task<int> AddRepairOrderAsync(RepairOrdersViewModal RepairOrder);
        Task<int> UpdateRepairOrderAsync(int RepairOrdersId, RepairOrdersViewModal RepairOrder);
        Task<int> DeleteRepairOrderAsync(int RepairOrdersId);
        Task<List<RepairOrders>> SearchRepairOrderAsync(string searchString);
    }
}
