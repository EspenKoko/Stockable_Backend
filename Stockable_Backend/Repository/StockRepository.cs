using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _appDbContext;

        public StockRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Stock
        //Get stock
        public async Task<Stock> GetStockAsync(int stockId)
        {
            Stock stock = await _appDbContext.Stocks.Include(b => b.stockType)
                .ThenInclude(city => city.stockCategory).FirstOrDefaultAsync(x => x.stockId == stockId);

            return stock;
        }

        //Get all Stock items
        public async Task<Stock[]> GetAllStocksAsync()
        {
            IQueryable<Stock> query = _appDbContext.Stocks.Include(c => c.stockType).ThenInclude(city => city.stockCategory);
            return await query.ToArrayAsync();
        }

        //add Stock Item
        public async Task<int> AddStockAsync(StockViewModal stock)
        {
            try
            {
                StockType stockType = await _appDbContext.StockTypes.FindAsync(stock.stockTypeId);
                if (stockType != null)
                {
                    Stock stockAdd = new Stock
                    {
                        stockName = stock.stockName,
                        qtyOnHand = stock.qtyOnHand,
                        stockDescription = stock.stockDescription,
                        stockTypeId = stock.stockTypeId,
                        minStockThreshold = stock.minStockThreshold,
                        maxStockThreshold = stock.maxStockThreshold,
                        //image = ConvertImageToBase64(stock.image),
                    };

                    await _appDbContext.Stocks.AddAsync(stockAdd);
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

        private string ConvertImageToBase64(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }

        // Update Stock
        public async Task<int> UpdateStockAsync(int stockId, StockViewModal stock)
        {
            try
            {
                Stock attemptToFindInDb = await _appDbContext.Stocks.FindAsync(stockId);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                attemptToFindInDb.stockName = stock.stockName;
                attemptToFindInDb.stockDescription = stock.stockDescription;
                attemptToFindInDb.qtyOnHand = stock.qtyOnHand;
                attemptToFindInDb.minStockThreshold = stock.minStockThreshold;
                attemptToFindInDb.maxStockThreshold = stock.maxStockThreshold;
                //attemptToFindInDb.image = ConvertImageToBase64(stock.image);


                StockType stockType = await _appDbContext.StockTypes.FindAsync(stock.stockTypeId);
                if (stockType != null)
                {
                    attemptToFindInDb.stockTypeId = stock.stockTypeId;
                    await _appDbContext.SaveChangesAsync();

                    return 200;
                }
                else
                {
                    return 501;
                }
            }
            catch (Exception)
            {
                return 500;
            }
        }


        // Delete Stock
        public async Task<int> DeleteStockAsync(int stockId)
        {
            Stock attemptToFindInDb = await _appDbContext.Stocks.FindAsync(stockId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.Stocks.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search Stock
        public async Task<List<Stock>> SearchStockAsync(string searchString)
        {
            List<Stock> stocks = await _appDbContext.Stocks.Include(b => b.stockType)
                .ThenInclude(city => city.stockCategory)
                .Where(x => x.stockName.Contains(searchString))
                .ToListAsync();

            return stocks;
        }
    }
}
