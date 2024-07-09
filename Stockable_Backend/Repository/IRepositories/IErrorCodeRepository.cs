using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IErrorCodeRepository
    {
        //ErrorCode
        Task<ErrorCode> GetErrorCodeAsync(int errorCodeId);
        Task<ErrorCode[]> GetAllErrorCodesAsync();
        Task<int> AddErrorCodeAsync(ErrorCodeViewModal errorCode);
        Task<int> UpdateErrorCodeAsync(int errorCodeId, ErrorCodeViewModal errorCode);
        Task<int> DeleteErrorCodeAsync(int errorCodeId);
        Task<List<ErrorCode>> SearchErrorCodeAsync(string searchString);
    }
}
