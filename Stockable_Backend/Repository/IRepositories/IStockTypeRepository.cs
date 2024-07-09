using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IStockTypeRepository
    {
        //StockType
        Task<StockType> GetStockTypeAsync(int stockTypeId);
        Task<StockType[]> GetAllStockTypesAsync();
        Task<int> AddStockTypeAsync(StockTypeViewModal stockType);
        Task<int> UpdateStockTypeAsync(int stockTypeId, StockTypeViewModal stockType);
        Task<int> DeleteStockTypeAsync(int stockTypeId);
        Task<List<StockType>> SearchStockTypeAsync(string searchString);

    }
}
