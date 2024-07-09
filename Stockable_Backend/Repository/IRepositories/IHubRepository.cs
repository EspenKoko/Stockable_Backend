using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IHubRepository
    {
        //Hub
        Task<Hub[]> GetAllHubsAsync();
        Task<Hub> GetHubAsync(int hubId);
        Task<int> AddHubAsync(HubViewModal hub);
        Task<int> UpdateHubAsync(int hubId, HubViewModal hub);
        Task<int> DeleteHubAsync(int hubId);
        Task<List<Hub>> SearchHubAsync(string searchString);
    }
}
