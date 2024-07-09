using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IHubUserRepository
    {
        //HubUser
        Task<HubUser> GetHubUserAsync(int hubUserId);
        Task<HubUser[]> GetAllHubUserAsync();
        Task<int> AddHubUserAsync(HubUserViewModal hubUser);
        Task<int> UpdateHubUserAsync(int hubUserId, HubUserViewModal hubUser);
        Task<int> DeleteHubUserAsync(int hubUserId);
        Task<List<HubUser>> SearchHubUserAsync(string searchString);

    }
}
