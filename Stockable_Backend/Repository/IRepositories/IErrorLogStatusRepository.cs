using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IErrorLogStatusRepository
    {
        //ErrorLogStatus
        Task<ErrorLogStatus[]> GetAllErrorLogStatusAsync();
        Task<ErrorLogStatus> GetErrorLogStatusAsync(int errorLogStatusId);
        Task<int> AddErrorLogStatusAsync(ErrorLogStatusViewModal errorLogStatus);
        Task<int> UpdateErrorLogStatusAsync(int errorLogStatusId, ErrorLogStatusViewModal errorLogStatus);
        Task<int> DeleteErrorLogStatusAsync(int errorLogStatusId);
        Task<List<ErrorLogStatus>> SearchErrorLogStatusAsync(string searchString);
    }
}
