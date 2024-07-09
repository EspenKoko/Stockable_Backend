using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IRepairStatusRepository
    {
        //RepairStatus
        Task<RepairStatus[]> GetAllRepairStatusAsync();
        Task<RepairStatus> GetRepairStatusAsync(int repairStatusId);
        Task<int> AddRepairStatusAsync(RepairStatusViewModal repairStatus);
        Task<int> UpdateRepairStatusAsync(int repairStatusId, RepairStatusViewModal repairStatus);
        Task<int> DeleteRepairStatusAsync(int repairStatusId);
        Task<List<RepairStatus>> SearchRepairStatusAsync(string searchString);
    }
}
