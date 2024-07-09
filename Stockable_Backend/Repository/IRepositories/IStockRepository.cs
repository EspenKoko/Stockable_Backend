using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IStockRepository
    {
        //Stock
        Task<Stock> GetStockAsync(int stockId);
        Task<Stock[]> GetAllStocksAsync();
        Task<int> AddStockAsync(StockViewModal stock);
        Task<int> UpdateStockAsync(int stockId, StockViewModal stock);
        Task<int> DeleteStockAsync(int stockId);
        Task<List<Stock>> SearchStockAsync(string searchString);
    }
}
