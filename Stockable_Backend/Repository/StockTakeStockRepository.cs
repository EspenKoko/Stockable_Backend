using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class StockTakeStockRepository : IStockTakeStockRepository
    {
        private readonly AppDbContext _appDbContext;

        public StockTakeStockRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Get all stock take stocks
        public async Task<StockTakeStock[]> GetAllStockTakeStocksAsync()
        {
            IQueryable<StockTakeStock> query = _appDbContext.StockTakeStocks
             .Include(c => c.stock).ThenInclude(stock => stock.stockType).ThenInclude(stockType => stockType.stockCategory)
             .Include(c => c.stockTake).ThenInclude(stockTake => stockTake.employee.user)
             .Include(c => c.stockTake).ThenInclude(stockTake => stockTake.employee.employeeType);

            return await query.ToArrayAsync();
        }

        //Get stockTakeStock
        public async Task<StockTakeStock> GetStockTakeStockAsync(int stockId)
        {
            StockTakeStock stockTakeStock = await _appDbContext.StockTakeStocks
                .Include(c => c.stock).ThenInclude(stock => stock.stockType).ThenInclude(stockType => stockType.stockCategory)
                .Include(c => c.stockTake).ThenInclude(stockTake => stockTake.employee.user)
                .Include(c => c.stockTake).ThenInclude(stockTake => stockTake.employee.employeeType)
                .FirstOrDefaultAsync(x => x.stockId == stockId);

            return stockTakeStock;
        }

        // Create stock take stock
        public async Task<int> AddStockTakeStockAsync(StockTakeStockViewModal stockTakeStock)
        {
            try
            {
                Stock stock = await _appDbContext.Stocks.FindAsync(stockTakeStock.stockId);
                StockTake stockTake = await _appDbContext.StockTakes.FindAsync(stockTakeStock.stockTakeId);


                if (stock != null && stockTake != null)
                {
                    StockTakeStock stockTakeStockAdd = new StockTakeStock
                    {
                        stockId = stockTakeStock.stockId,
                        stockTakeId = stockTakeStock.stockId,
                        qty = stockTakeStock.qty
                    };

                    await _appDbContext.StockTakeStocks.AddAsync(stockTakeStockAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Stock or StockTake not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while saving to the database: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                return 500; // Internal server error
            }
        }

        // Update stock take stock
        public async Task<int> UpdateStockTakeStockAsync(int stockId, StockTakeStockViewModal stockTakeStock)
        {
            try
            {
                // Find the object in the db 
                StockTakeStock attemptToFindInDb = await _appDbContext.StockTakeStocks.FirstOrDefaultAsync(x => x.stockId == stockId);

                if (attemptToFindInDb == null)
                {
                    return 404; // StockTakeStock not found
                }

                Stock stock = await _appDbContext.Stocks.FindAsync(stockTakeStock.stockId);
                StockTake stockTake = await _appDbContext.StockTakes.FindAsync(stockTakeStock.stockTakeId);

                if (stock != null && stockTake != null)
                {
                    attemptToFindInDb.stockId = stockTakeStock.stockId;
                    attemptToFindInDb.stockTakeId = stockTakeStock.stockTakeId;
                    attemptToFindInDb.qty = stockTakeStock.qty;

                    _appDbContext.StockTakeStocks.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid Stock or PurchaseOrder
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }
        }

        // Delete stock take stock
        public async Task<int> DeleteStockTakeStockAsync(int stockId)
        {
            StockTakeStock stockTakeStockToDelete = await _appDbContext.StockTakeStocks.FirstOrDefaultAsync(sts => sts.stockId == stockId);

            if (stockTakeStockToDelete == null)
            {
                return 404; // Stock take stock not found
            }

            _appDbContext.StockTakeStocks.Remove(stockTakeStockToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search stockTakeStock
        public async Task<List<StockTakeStock>> SearchStockTakeStockAsync(string searchString)
        {
            List<StockTakeStock> stockTakes = await _appDbContext.StockTakeStocks
                .Include(c => c.stock).ThenInclude(stock => stock.stockType).ThenInclude(stockType => stockType.stockCategory)
                .Include(c => c.stockTake).ThenInclude(stockTake => stockTake.employee.user)
                .Include(c => c.stockTake).ThenInclude(stockTake => stockTake.employee.employeeType)
                .Where(x => x.stockTake.employee.user.userFirstName.Contains(searchString)).ToListAsync();

            return stockTakes;
        }
    }

}
