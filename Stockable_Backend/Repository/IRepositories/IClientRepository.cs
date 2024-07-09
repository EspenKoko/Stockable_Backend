using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IClientRepository
    {
        //Client
        Task<Client[]> GetAllClientsAsync();
        Task<Client> GetClientAsync(int clientId);
        Task<int> AddClientAsync(ClientViewModal client);
        Task<int> UpdateClientAsync(int clientId, ClientViewModal client);
        Task<int> DeleteClientAsync(int clientId);
        Task<List<Client>> SearchClientAsync(string searchString);
    }
}
