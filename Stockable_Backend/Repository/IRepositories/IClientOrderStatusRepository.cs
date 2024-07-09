using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IClientOrderStatusRepository
    {
        //ClientOrderStatus
        Task<ClientOrderStatus[]> GetAllClientOrderStatussAsync();
        Task<ClientOrderStatus> GetClientOrderStatusAsync(int clientOrderStatusId);
        Task<int> AddClientOrderStatusAsync(ClientOrderStatusViewModal clientOrderStatus);
        Task<int> UpdateClientOrderStatusAsync(int clientOrderStatusId, ClientOrderStatusViewModal clientOrderStatus);
        Task<int> DeleteClientOrderStatusAsync(int clientOrderStatusId);
        Task<List<ClientOrderStatus>> SearchClientOrderStatusAsync(string searchString);
    }
}
