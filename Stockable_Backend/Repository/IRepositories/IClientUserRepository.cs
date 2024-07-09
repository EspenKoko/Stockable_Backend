using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IClientUserRepository
    {
        //ClientUser
        Task<ClientUser[]> GetAllClientUsersAsync();
        Task<ClientUser> GetClientUserAsync(int clientUserId);
        Task<int> AddClientUserAsync(ClientUserViewModal clientUser);
        Task<int> UpdateClientUserAsync(int clientUserId, ClientUserViewModal clientUser);
        Task<int> DeleteClientUserAsync(int clientUserId);
        Task<List<ClientUser>> SearchClientUserAsync(string searchString);
    }
}
