using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class PriceRepository : IPriceRepository
    {
        private readonly AppDbContext _appDbContext;

        public PriceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //Price
        //Get Price
        public async Task<Price> GetPriceAsync(int priceId)
        {
            Price price = await _appDbContext.Prices.Include(x => x.stock).ThenInclude(x => x.stockType).ThenInclude(x => x.stockCategory)
                .FirstOrDefaultAsync(x => x.priceId == priceId);

            return price;
        }

        //Get all Prices
        public async Task<Price[]> GetAllPriceAsync()
        {
            IQueryable<Price> query = _appDbContext.Prices.Include(c => c.stock).ThenInclude(x => x.stockType).ThenInclude(x => x.stockCategory);
            return await query.ToArrayAsync();
        }

        //Create Price
        public async Task<int> AddPriceAsync(PriceViewModal price)
        {
            try
            {
                Price priceAdd = new Price
                {
                    price = price.price,
                    priceDate = price.priceDate
                };

                Stock stock = await _appDbContext.Stocks.FindAsync(price.stockId);
                if (stock != null)
                {
                    priceAdd.stockId = price.stockId;
                    await _appDbContext.Prices.AddAsync(priceAdd);
                    await _appDbContext.SaveChangesAsync();
                    return 200;
                }
                else
                {
                    return 404;
                }
            }
            catch (Exception)
            {
                return 500;
            }
        }

        // Update Price
        public async Task<int> UpdatePriceAsync(int priceId, PriceViewModal price)
        {
            try
            {
                Price attemptToFindInDb = await _appDbContext.Prices.FindAsync(priceId);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                attemptToFindInDb.price = price.price;
                attemptToFindInDb.priceDate = price.priceDate;

                Stock stock = await _appDbContext.Stocks.FindAsync(price.stockId);
                if (stock != null)
                {
                    attemptToFindInDb.stockId = price.stockId;
                    _appDbContext.Prices.Update(attemptToFindInDb);
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


        // Delete Price
        public async Task<int> DeletePriceAsync(int priceId)
        {
            try
            {
                Price attemptToFindInDb = await _appDbContext.Prices.FindAsync(priceId);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                _appDbContext.Prices.Remove(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();
                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        //Search branch
        public async Task<List<Price>> SearchPriceAsync(string searchString)
        {
            List<Price> prices = await _appDbContext.Prices.Include(c => c.stock).ThenInclude(x => x.stockType).ThenInclude(x => x.stockCategory).Where(x => x.stock.stockName.Contains(searchString)).ToListAsync();

            return prices;
        }

    }
}
