using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class StockCategoryRepository : IStockCategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public StockCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //StockCategory
        //Get StockCategory
        public async Task<StockCategory> GetStockCategoryAsync(int stockCategoryId)
        {

            StockCategory stockCategory = await _appDbContext.StockCategories.Where(x => x.stockCategoryId == stockCategoryId).FirstOrDefaultAsync();

            return stockCategory;
        }

        //Get all StockCategory
        public async Task<StockCategory[]> GetAllStockCategoriesAsync()
        {
            IQueryable<StockCategory> query = _appDbContext.StockCategories;
            return await query.ToArrayAsync();
        }

        // Create StockCategory
        public async Task<int> AddStockCategoryAsync(StockCategoryViewModal stockCategory)
        {
            try
            {
                StockCategory stockCategoryAdd = new StockCategory
                {
                    stockCategoryName = stockCategory.stockCategoryName
                };

                await _appDbContext.StockCategories.AddAsync(stockCategoryAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update StockCategory
        public async Task<int> UpdateStockCategoryAsync(int stockCategoryId, StockCategoryViewModal stockCategory)
        {
            StockCategory attemptToFindInDb = await _appDbContext.StockCategories.FindAsync(stockCategoryId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            attemptToFindInDb.stockCategoryName = stockCategory.stockCategoryName;

            _appDbContext.StockCategories.Update(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Delete StockCategory
        public async Task<int> DeleteStockCategoryAsync(int stockCategoryId)
        {
            StockCategory attemptToFindInDb = await _appDbContext.StockCategories.FindAsync(stockCategoryId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.StockCategories.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search StockCategory
        public async Task<List<StockCategory>> SearchStockCategoryAsync(string searchString)
        {
            List<StockCategory> stockCategories = await _appDbContext.StockCategories
                .Where(x => x.stockCategoryName.Contains(searchString))
                .ToListAsync();

            return stockCategories;
        }

    }
}
