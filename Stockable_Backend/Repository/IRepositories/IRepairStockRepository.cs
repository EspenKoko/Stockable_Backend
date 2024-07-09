using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IRepairStockRepository
    {
        //RepairStock
        Task<RepairStock[]> GetAllRepairStockAsync();
        Task<List<RepairStock>> GetRepairStockAsync(int repairId);
        Task<int> AddRepairStockAsync(RepairStockViewModal repairStock);
        Task<int> UpdateRepairStockAsync(int repairId, RepairStockViewModal repairStock);
        Task<int> DeleteRepairStockAsync(int repairId);
        Task<List<RepairStock>> SearchRepairStockAsync(string searchString);
    }
}
