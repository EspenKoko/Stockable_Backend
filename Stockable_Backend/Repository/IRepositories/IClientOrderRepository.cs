using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IClientOrderRepository
    {
        //ClientOrder
        Task<ClientOrder[]> GetAllClientOrderAsync();
        Task<ClientOrder> GetClientOrderAsync(int stockOrderId);
        Task<int> AddClientOrderAsync(ClientOrderViewModal stockOrder);
        Task<int> UpdateClientOrderAsync(int stockOrderId, ClientOrderViewModal stockOrder);
        Task<int> DeleteClientOrderAsync(int stockOrderId);
        Task<List<ClientOrder>> SearchClientOrderAsync(string searchString);
    }
}
