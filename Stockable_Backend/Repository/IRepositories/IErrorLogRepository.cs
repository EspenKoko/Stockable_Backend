using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IErrorLogRepository
    {
        //ErrorLog
        Task<ErrorLog[]> GetAllErrorLogAsync();
        Task<ErrorLog> GetErrorLogAsync(int errorLogId);
        Task<int> AddErrorLogAsync(ErrorLogViewModal errorLog);
        Task<int> UpdateErrorLogAsync(int errorLogId, ErrorLogViewModal errorLog);
        Task<int> DeleteErrorLogAsync(int errorLogId);
        Task<List<ErrorLog>> SearchErrorLogAsync(string searchString);
    }
}
