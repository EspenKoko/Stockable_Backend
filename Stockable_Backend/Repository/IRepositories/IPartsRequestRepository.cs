using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IPartsRequestRepository
    {
        //PartsRequest
        Task<PartsRequest[]> GetAllPartsRequestAsync();
        Task<List<PartsRequest>> GetPartsRequestAsync(int invoiceId);
        Task<int> AddPartsRequestAsync(PartsRequestViewModal partsRequest);
        Task<int> UpdatePartsRequestAsync(int invoiceId, PartsRequestViewModal partsRequest);
        Task<int> DeletePartsRequestAsync(int invoiceId);
        Task<List<PartsRequest>> SearchPartsRequestAsync(string searchString);
    }
}
