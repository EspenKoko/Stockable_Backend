using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface ITechnicalServiceReportRepository
    {
        //TechnicalServiceReport
        Task<TechnicalServiceReport[]> GetAllTechnicalServiceReportAsync();
        Task<TechnicalServiceReport> GetTechnicalServiceReportAsync(int technicalServiceReportId);
        Task<int> AddTechnicalServiceReportAsync(TechnicalServiceReportViewModal technicalServiceReport);
        Task<int> UpdateTechnicalServiceReportAsync(int technicalServiceReportId, TechnicalServiceReportViewModal technicalServiceReport);
        Task<int> DeleteTechnicalServiceReportAsync(int technicalServiceReportId);
        Task<List<TechnicalServiceReport>> SearchTechnicalServiceReportAsync(string searchString);
    }
}
