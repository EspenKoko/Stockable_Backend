using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class RepairStockRepository : IRepairStockRepository
    {
        private readonly AppDbContext _appDbContext;

        public RepairStockRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //RepairStock
        //Get all erepairStock
        public async Task<RepairStock[]> GetAllRepairStockAsync()
        {
            IQueryable<RepairStock> query = _appDbContext.RepairStocks
            .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
            .Include(c => c.repair).ThenInclude(repair => repair.repairStatus)
            .Include(c => c.repair).ThenInclude(repair => repair.employee.employeeType)
            .Include(c => c.repair).ThenInclude(repair => repair.employee.user)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorCode)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.branch)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
            //.Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.client)
            .Include(c => c.purchaseOrder.purchaseOrderStatus);

            return await query.ToArrayAsync();
        }

        //Get repairStock
        public async Task<List<RepairStock>> GetRepairStockAsync(int repairId)
        {
            List<RepairStock> repairStocks = await _appDbContext.RepairStocks
                .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
                .Include(c => c.repair).ThenInclude(repair => repair.repairStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.employee.employeeType)
                .Include(c => c.repair).ThenInclude(repair => repair.employee.user)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorCode)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.branch)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
                //.Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.client)
                .Include(c => c.purchaseOrder.purchaseOrderStatus)
                .Where(x => x.repairId == repairId)
                .ToListAsync();

            return repairStocks;
        }

        //Create repairStock
        public async Task<int> AddRepairStockAsync(RepairStockViewModal repairStock)
        {
            try
            {
                Stock stock = await _appDbContext.Stocks.FindAsync(repairStock.stockId);
                Repair repair = await _appDbContext.Repairs.FindAsync(repairStock.repairId);
                PurchaseOrder purchaseOrder= await _appDbContext.PurchaseOrders.FindAsync(repairStock.purchaseOrderId);


                if (stock != null && repair != null && purchaseOrder != null)
                {
                    RepairStock repairStockAdd = new RepairStock
                    {
                        stockId = repairStock.stockId,
                        repairId = repairStock.repairId,
                        purchaseOrderId = repairStock.purchaseOrderId,
                        qty = repairStock.qty
                    };

                    await _appDbContext.RepairStocks.AddAsync(repairStockAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Stock or PurchaseOrder not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while saving to the database: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                return 500; // Internal server error
            }
        }

        //Update repairStock
        public async Task<int> UpdateRepairStockAsync(int repairId, RepairStockViewModal repairStock)
        {
            try
            {
                // Find the object in the db 
                RepairStock attemptToFindInDb = await _appDbContext.RepairStocks.FirstOrDefaultAsync(x => x.repairId == repairId);

                if (attemptToFindInDb == null)
                {
                    return 404; // RepairStock not found
                }

                Stock stock = await _appDbContext.Stocks.FindAsync(repairStock.stockId);
                Repair repair = await _appDbContext.Repairs.FindAsync(repairStock.repairId);
                PurchaseOrder purchaseOrder = await _appDbContext.PurchaseOrders.FindAsync(repairStock.purchaseOrderId);


                if (stock != null && repair != null && purchaseOrder != null)
                {
                    attemptToFindInDb.stockId = repairStock.stockId;
                    attemptToFindInDb.repairId = repairStock.repairId;
                    attemptToFindInDb.purchaseOrderId = repairStock.purchaseOrderId;
                    attemptToFindInDb.qty = repairStock.qty;

                    _appDbContext.RepairStocks.Update(attemptToFindInDb);
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


        //Delete repairStock
        public async Task<int> DeleteRepairStockAsync(int repairId)
        {
            // Find the object in the db 
            RepairStock repairStockToDelete = await _appDbContext.RepairStocks.FirstOrDefaultAsync(x => x.repairId == repairId);

            if (repairStockToDelete == null)
            {
                return 404; // RepairStock not found
            }

            _appDbContext.RepairStocks.Remove(repairStockToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search repairStock
        public async Task<List<RepairStock>> SearchRepairStockAsync(string searchString)
        {
            List<RepairStock> repairStockes = await _appDbContext.RepairStocks
                .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
                .Include(c => c.repair).ThenInclude(repair => repair.repairStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.employee.employeeType)
                .Include(c => c.repair).ThenInclude(repair => repair.employee.user)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorCode)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.branch)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
                //.Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.client)
                .Include(c => c.purchaseOrder.purchaseOrderStatus)
                .Where(x => x.repair.errorLog.assignedPrinter.serialNumber.Contains(searchString)).ToListAsync();

            return repairStockes;
        }
    }
}
