using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IRepairDiagnosticRepository
    {
        //RepairDiagnostics
        Task<RepairDiagnostic[]> GetAllRepairDiagnosticsAsync();
        Task<RepairDiagnostic> GetRepairDiagnosticsAsync(int repairId);
        Task<int> AddRepairDiagnosticsAsync(RepairDiagnosticViewModal repairDiagnostics);
        Task<int> UpdateRepairDiagnosticsAsync(int repairId, RepairDiagnosticViewModal repairDiagnostics);
        Task<int> DeleteRepairDiagnosticsAsync(int repairId);
        Task<List<RepairDiagnostic>> SearchRepairDiagnosticsAsync(string searchString);
    }
}
