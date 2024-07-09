using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class PartsRequestRepository : IPartsRequestRepository
    {
        private readonly AppDbContext _appDbContext;

        public PartsRequestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //PartsRequest
        //Get all epartsRequest
        public async Task<PartsRequest[]> GetAllPartsRequestAsync()
        {
            IQueryable<PartsRequest> query = _appDbContext.PartsRequests
            .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.repairStatus)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.employee.employeeType)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.employee.user)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.errorLogStatus)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.errorCode)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.client)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.branch)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.user)
            //.Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.printerModel)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.printerStatus)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.client);

            return await query.ToArrayAsync();
        }

        //Get partsRequest
        public async Task<List<PartsRequest>> GetPartsRequestAsync(int purchaseOrderId)
        {
            List<PartsRequest> partsRequests = await _appDbContext.PartsRequests
            .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.repairStatus)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.employee.employeeType)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.employee.user)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.errorLogStatus)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.errorCode)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.client)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.branch)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.user)
            //.Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.printerModel)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.printerStatus)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.client)
            .Where(x => x.purchaseOrderId == purchaseOrderId)
            .ToListAsync();

            return partsRequests;
        }

        //Create partsRequest
        public async Task<int> AddPartsRequestAsync(PartsRequestViewModal partsRequest)
        {
            try
            {
                Stock stock = await _appDbContext.Stocks.FindAsync(partsRequest.stockId);
                PurchaseOrder purchaseOrder = await _appDbContext.PurchaseOrders.FindAsync(partsRequest.purchaseOrderId);


                if (stock != null && purchaseOrder != null)
                {
                    PartsRequest partsRequestAdd = new PartsRequest
                    {
                        stockId = partsRequest.stockId,
                        purchaseOrderId = partsRequest.purchaseOrderId,
                        qty = partsRequest.qty
                    };

                    await _appDbContext.PartsRequests.AddAsync(partsRequestAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Stock or Repair Invoice not found
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update partsRequest
        public async Task<int> UpdatePartsRequestAsync(int purchaseOrderId, PartsRequestViewModal partsRequest)
        {
            try
            {
                // Find the object in the db 
                PartsRequest attemptToFindInDb = await _appDbContext.PartsRequests.FirstOrDefaultAsync(x => x.purchaseOrderId == purchaseOrderId);

                if (attemptToFindInDb == null)
                {
                    return 404; // PartsRequest not found
                }

                Stock stock = await _appDbContext.Stocks.FindAsync(partsRequest.stockId);
                PurchaseOrder purchaseOrder = await _appDbContext.PurchaseOrders.FindAsync(partsRequest.purchaseOrderId);

                if (stock != null && purchaseOrder != null)
                {
                    attemptToFindInDb.stockId = partsRequest.stockId;
                    attemptToFindInDb.purchaseOrderId = partsRequest.purchaseOrderId;
                    attemptToFindInDb.qty = partsRequest.qty;

                    _appDbContext.PartsRequests.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid Stock or Repair Invoice
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }


        //Delete partsRequest
        public async Task<int> DeletePartsRequestAsync(int purchaseOrderId)
        {
            // Find the object in the db 
            PartsRequest partsRequestToDelete = await _appDbContext.PartsRequests.FirstOrDefaultAsync(x => x.purchaseOrderId == purchaseOrderId);

            if (partsRequestToDelete == null)
            {
                return 404; // PartsRequest not found
            }

            _appDbContext.PartsRequests.Remove(partsRequestToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search partsRequest
        public async Task<List<PartsRequest>> SearchPartsRequestAsync(string searchString)
        {
            List<PartsRequest> partsRequestes = await _appDbContext.PartsRequests
                .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.repairStatus)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.employee.employeeType)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.employee.user)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.errorLogStatus)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.errorCode)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.client)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.branch)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.clientUser.user)
                //.Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.printerModel)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.printerStatus)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair.errorLog.assignedPrinter.client)
                .Where(x => x.purchaseOrder.repair.errorLog.assignedPrinter.serialNumber.Contains(searchString)).ToListAsync();

            return partsRequestes;
        }
    }
}
