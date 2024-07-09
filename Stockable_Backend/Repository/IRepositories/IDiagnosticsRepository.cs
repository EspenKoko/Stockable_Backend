using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IDiagnosticsRepository
    {
         //Diagnostics
        Task<Diagnostics[]> GetAllDiagnosticsAsync();
        Task<Diagnostics> GetDiagnosticsAsync(int diagnosticsId);
        Task<int> AddDiagnosticsAsync(DiagnosticsViewModal diagnostics);
        Task<int> UpdateDiagnosticsAsync(int diagnosticsId, DiagnosticsViewModal diagnostics);
        Task<int> DeleteDiagnosticsAsync(int diagnosticsId);
        Task<List<Diagnostics>> SearchDiagnosticsAsync(string searchString);
    }
}
