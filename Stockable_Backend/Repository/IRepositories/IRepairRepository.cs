using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IRepairRepository
    {
        //Repair
        Task<Repair[]> GetAllRepairAsync();
        Task<Repair> GetRepairAsync(int repairId);
        Task<int> AddRepairAsync(RepairViewModal repair);
        Task<int> UpdateRepairAsync(int repairId, RepairViewModal repair);
        Task<int> DeleteRepairAsync(int repairId);
        Task<List<Repair>> SearchRepairAsync(string searchString);
    }
}
