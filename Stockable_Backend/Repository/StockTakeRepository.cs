using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class StockTakeRepository : IStockTakeRepository
    {
        private readonly AppDbContext _appDbContext;

        public StockTakeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Get all stock takes
        public async Task<StockTake[]> GetAllStockTakesAsync()
        {
            IQueryable<StockTake> query = _appDbContext.StockTakes
            .Include(c => c.employee).ThenInclude(employee => employee.user);

            return await query.ToArrayAsync();
        }

        // Get stock take
        public async Task<StockTake> GetStockTakeAsync(int stockTakeId)
        {
            return await _appDbContext.StockTakes
                .Include(c => c.employee).ThenInclude(employee => employee.user)
                .FirstOrDefaultAsync(st => st.stockTakeId == stockTakeId);
        }

        // Create stock take
        public async Task<int> AddStockTakeAsync(StockTakeViewModal stockTake)
        {
            try
            {
                Employee employee = await _appDbContext.Employees.FindAsync(stockTake.employeeId);

                if (employee != null)
                {
                    StockTake stockTakeAdd = new StockTake
                    {
                        stockTakeDate = stockTake.stockTakeDate,
                        employeeId = stockTake.employeeId,
                    };

                    await _appDbContext.StockTakes.AddAsync(stockTakeAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Employee not found
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update stockTake
        public async Task<int> UpdateStockTakeAsync(int stockTakeId, StockTakeViewModal stockTake)
        {
            try
            {
                // Find the object in the db 
                StockTake attemptToFindInDb = await _appDbContext.StockTakes.FirstOrDefaultAsync(x => x.stockTakeId == stockTakeId);

                if (attemptToFindInDb == null)
                {
                    return 404; // StockTake not found
                }

                attemptToFindInDb.stockTakeDate = stockTake.stockTakeDate;

                Employee employee = await _appDbContext.Employees.FindAsync(stockTake.employeeId);

                if (employee != null)
                {
                    attemptToFindInDb.employeeId = stockTake.employeeId;

                    _appDbContext.StockTakes.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid client or city
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete stockTake
        public async Task<int> DeleteStockTakeAsync(int stockTakeId)
        {
            // Find the object in the db 
            StockTake stockTakeToDelete = await _appDbContext.StockTakes.FirstOrDefaultAsync(x => x.stockTakeId == stockTakeId);

            if (stockTakeToDelete == null)
            {
                return 404; // StockTake not found
            }

            _appDbContext.StockTakes.Remove(stockTakeToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search stockTake
        public async Task<List<StockTake>> SearchStockTakeAsync(string searchString)
        {
            List<StockTake> stockTakees = await _appDbContext.StockTakes
                .Include(c => c.employee).ThenInclude(employee => employee.user)
                .Where(x => x.employee.user.userFirstName.Contains(searchString)).ToListAsync();

            return stockTakees;
        }
    }

}
