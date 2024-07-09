using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IStockTakeStockRepository
    {
        Task<StockTakeStock[]> GetAllStockTakeStocksAsync();
        Task<StockTakeStock> GetStockTakeStockAsync(int stockId);
        Task<int> AddStockTakeStockAsync(StockTakeStockViewModal stockTakeStock);
        Task<int> UpdateStockTakeStockAsync(int stockId, StockTakeStockViewModal stockTakeStock);
        Task<int> DeleteStockTakeStockAsync(int stockId);
        Task<List<StockTakeStock>> SearchStockTakeStockAsync(string searchString);
    }

}
