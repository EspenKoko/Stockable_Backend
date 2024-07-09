using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IStockTakeRepository
    {
        Task<StockTake[]> GetAllStockTakesAsync();
        Task<StockTake> GetStockTakeAsync(int stockTakeId);
        Task<int> AddStockTakeAsync(StockTakeViewModal stockTake);
        Task<int> UpdateStockTakeAsync(int stockTakeId, StockTakeViewModal stockTake);
        Task<int> DeleteStockTakeAsync(int stockTakeId);
        Task<List<StockTake>> SearchStockTakeAsync(string searchString);
    }

}
