using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class StockSupplierOrderRepository : IStockSupplierOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public StockSupplierOrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //StockSupplierOrder
        //Get all estockSupplierOrder
        public async Task<StockSupplierOrder[]> GetAllStockSupplierOrderAsync()
        {
            IQueryable<StockSupplierOrder> query = _appDbContext.StockSupplierOrders
            .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
            .Include(c => c.supplierOrder).ThenInclude(stock => stock.supplierOrderStatus)
            .Include(c => c.supplierOrder).ThenInclude(stock => stock.supplier)
            .Include(c => c.supplierOrder).ThenInclude(stock => stock.employee.employeeType)
            .Include(c => c.supplierOrder).ThenInclude(stock => stock.employee.user);

            return await query.ToArrayAsync();
        }

        //Get stockSupplierOrder
        public async Task<StockSupplierOrder> GetStockSupplierOrderAsync(int supplierOrderId)
        {
            StockSupplierOrder stockSupplierOrder = await _appDbContext.StockSupplierOrders
                .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
                .Include(c => c.supplierOrder).ThenInclude(stock => stock.supplierOrderStatus)
                .Include(c => c.supplierOrder).ThenInclude(stock => stock.supplier)
                .Include(c => c.supplierOrder).ThenInclude(stock => stock.employee.employeeType)
                .Include(c => c.supplierOrder).ThenInclude(stock => stock.employee.user)
                .FirstOrDefaultAsync(x => x.supplierOrderId == supplierOrderId);

            return stockSupplierOrder;
        }

        //Create stockSupplierOrder
        public async Task<int> AddStockSupplierOrderAsync(StockSupplierOrderViewModal stockSupplierOrder)
        {
            try
            {
                Stock stock = await _appDbContext.Stocks.FindAsync(stockSupplierOrder.stockId);
                SupplierOrder supplierOrder = await _appDbContext.SupplierOrders.FindAsync(stockSupplierOrder.supplierOrderId);


                if (stock != null && supplierOrder != null)
                {
                    StockSupplierOrder stockSupplierOrderAdd = new StockSupplierOrder
                    {
                        stockId = stockSupplierOrder.stockId,
                        supplierOrderId = stockSupplierOrder.supplierOrderId,
                        qty = stockSupplierOrder.qty
                    };

                    await _appDbContext.StockSupplierOrders.AddAsync(stockSupplierOrderAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Stock or PurchaseOrder not found
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update stockSupplierOrder
        public async Task<int> UpdateStockSupplierOrderAsync(int supplierOrderId, StockSupplierOrderViewModal stockSupplierOrder)
        {
            try
            {
                // Find the object in the db 
                StockSupplierOrder attemptToFindInDb = await _appDbContext.StockSupplierOrders.FirstOrDefaultAsync(x => x.supplierOrderId == supplierOrderId);

                if (attemptToFindInDb == null)
                {
                    return 404; // StockSupplierOrder not found
                }

                Stock stock = await _appDbContext.Stocks.FindAsync(stockSupplierOrder.stockId);
                SupplierOrder supplierOrder = await _appDbContext.SupplierOrders.FindAsync(stockSupplierOrder.supplierOrderId);


                if (stock != null && supplierOrder != null)
                {
                    attemptToFindInDb.stockId = stockSupplierOrder.stockId;
                    attemptToFindInDb.supplierOrderId = stockSupplierOrder.supplierOrderId;
                    attemptToFindInDb.qty = stockSupplierOrder.qty;

                    _appDbContext.StockSupplierOrders.Update(attemptToFindInDb);
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


        //Delete stockSupplierOrder
        public async Task<int> DeleteStockSupplierOrderAsync(int supplierOrderId)
        {
            // Find the object in the db 
            List<StockSupplierOrder> stockSupplierOrdersToDelete = await _appDbContext.StockSupplierOrders
            .Where(x => x.supplierOrderId == supplierOrderId)
            .ToListAsync();

            if (stockSupplierOrdersToDelete.Count == 0)
            {
                return 404; // StockSupplierOrder not found
            }

            _appDbContext.StockSupplierOrders.RemoveRange(stockSupplierOrdersToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        // Delete stockSupplierOrder based on supplierOrderId and stockItemId
        public async Task<int> DeleteStockSupplierOrderItemAsync(int supplierOrderId, int stockId)
        {
            // Find the object in the db with the given supplierOrderId and stockItemId
            StockSupplierOrder stockSupplierOrderToDelete = await _appDbContext.StockSupplierOrders
                .FirstOrDefaultAsync(x => x.supplierOrderId == supplierOrderId && x.stockId == stockId);

            if (stockSupplierOrderToDelete == null)
            {
                return 404; // StockSupplierOrder not found
            }

            _appDbContext.StockSupplierOrders.Remove(stockSupplierOrderToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        //Search stockSupplierOrder
        public async Task<List<StockSupplierOrder>> SearchStockSupplierOrderAsync(string searchString)
        {
            List<StockSupplierOrder> stockSupplierOrderes = await _appDbContext.StockSupplierOrders
                .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
                .Include(c => c.supplierOrder).ThenInclude(stock => stock.supplierOrderStatus)
                .Include(c => c.supplierOrder).ThenInclude(stock => stock.supplier)
                .Include(c => c.supplierOrder).ThenInclude(stock => stock.employee.employeeType)
                .Include(c => c.supplierOrder).ThenInclude(stock => stock.employee.user)
                .Where(x => x.stock.stockName.Contains(searchString)).ToListAsync();

            return stockSupplierOrderes;
        }
    }
}
