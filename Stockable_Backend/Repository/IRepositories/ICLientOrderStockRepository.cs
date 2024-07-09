using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IClientOrderStockRepository
    {
        //CLientOrderStock
        Task<ClientOrderStock[]> GetAllCLientOrderStockAsync();
        Task<List<ClientOrderStock>> GetCLientOrderStockAsync(int clientOrderId);
        Task<int> AddCLientOrderStockAsync(ClientOrderStockViewModal cLientOrderStock);
        Task<int> UpdateCLientOrderStockAsync(int clientOrderId, ClientOrderStockViewModal cLientOrderStock);
        Task<int> DeleteCLientOrderStockAsync(int clientOrderId);
        Task<int> DeleteCLientOrderStockItemAsync(int clientOrderId, int stockId);
        Task<List<ClientOrderStock>> SearchCLientOrderStockAsync(string searchString);
    }
}
