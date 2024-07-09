using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class StockTypeRepository : IStockTypeRepository
    {
        private readonly AppDbContext _appDbContext;

        public StockTypeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //StockType
        //Get StockType
        public async Task<StockType> GetStockTypeAsync(int stockTypeId)
        {
            StockType stockType = await _appDbContext.StockTypes.Include(c => c.stockCategory).FirstOrDefaultAsync(x => x.stockTypeId == stockTypeId);

            return stockType;
        }

        //Get all StockTypes
        public async Task<StockType[]> GetAllStockTypesAsync()
        {
            IQueryable<StockType> query = _appDbContext.StockTypes.Include(c => c.stockCategory);
            return await query.ToArrayAsync();
        }

        // Create StockType
        public async Task<int> AddStockTypeAsync(StockTypeViewModal stockType)
        {
            try
            {
                StockType stockTypeAdd = new StockType
                {
                    stockTypeName = stockType.stockTypeName
                };

                StockCategory stockCategory = await _appDbContext.StockCategories.FindAsync(stockType.stockCategoryId);
                if (stockCategory != null)
                {
                    stockTypeAdd.stockCategoryId = stockType.stockCategoryId;
                    await _appDbContext.StockTypes.AddAsync(stockTypeAdd);
                    await _appDbContext.SaveChangesAsync();
                    return 200;
                }
                else
                {
                    return 404;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update StockType
        public async Task<int> UpdateStockTypeAsync(int stockTypeId, StockTypeViewModal stockType)
        {
            try
            {
                StockType attemptToFindInDb = await _appDbContext.StockTypes.FindAsync(stockTypeId);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                attemptToFindInDb.stockTypeName = stockType.stockTypeName;

                StockCategory stockCategory = await _appDbContext.StockCategories.FindAsync(stockType.stockCategoryId);
                if (stockCategory != null)
                {
                    attemptToFindInDb.stockCategoryId = stockType.stockCategoryId;
                    _appDbContext.StockTypes.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();
                    return 200;
                }
                else
                {
                    return 501;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete StockType
        public async Task<int> DeleteStockTypeAsync(int stockTypeId)
        {
            StockType attemptToFindInDb = await _appDbContext.StockTypes.FindAsync(stockTypeId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.StockTypes.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search StockType
        public async Task<List<StockType>> SearchStockTypeAsync(string searchString)
        {
            List<StockType> stockTypes = await _appDbContext.StockTypes.Include(c => c.stockCategory)
                .Where(x => x.stockTypeName.Contains(searchString))
                .ToListAsync();

            return stockTypes;
        }

    }
}
