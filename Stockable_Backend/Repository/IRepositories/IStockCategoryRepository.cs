using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IStockCategoryRepository
    {
        //StockCategory
        Task<StockCategory> GetStockCategoryAsync(int stockCategoryId);
        Task<StockCategory[]> GetAllStockCategoriesAsync();
        Task<int> AddStockCategoryAsync(StockCategoryViewModal stockCategory);
        Task<int> UpdateStockCategoryAsync(int stockCategoryId, StockCategoryViewModal stockCategory);
        Task<int> DeleteStockCategoryAsync(int stockCategoryId);
        Task<List<StockCategory>> SearchStockCategoryAsync(string searchString);
    }
}
