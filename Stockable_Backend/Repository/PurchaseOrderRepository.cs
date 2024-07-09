using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public PurchaseOrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //PurchaseOrder
        //Get all purchaseOrders
        public async Task<PurchaseOrder[]> GetAllPurchaseOrderAsync()
        {
            IQueryable<PurchaseOrder> query = _appDbContext.PurchaseOrders
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.errorCode)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.repairStatus)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
            //.Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.client)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.employee).ThenInclude(employee => employee.user)
            .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.employee).ThenInclude(employee => employee.employeeType)
            .Include(c => c.purchaseOrderStatus);

            return await query.ToArrayAsync();
        }

        //Get purchaseOrder
        public async Task<PurchaseOrder> GetPurchaseOrderAsync(int purchaseOrderId)
        {
            PurchaseOrder purchaseOrder = await _appDbContext.PurchaseOrders
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.errorCode)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.repairStatus)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
                //.Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.client)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.employee).ThenInclude(employee => employee.user)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.employee).ThenInclude(employee => employee.employeeType)
            .Include(c => c.purchaseOrderStatus)
                .FirstOrDefaultAsync(x => x.purchaseOrderId == purchaseOrderId);

            return purchaseOrder;
        }

        //Create purchaseOrder
        public async Task<int> AddPurchaseOrderAsync(PurchaseOrderViewModal purchaseOrder)
        {
            try
            {
                Repair repair = await _appDbContext.Repairs.FindAsync(purchaseOrder.repairId);
                PurchaseOrderStatus purchaseOrderStatus = await _appDbContext.PurchaseOrderStatuses.FindAsync(purchaseOrder.purchaseOrderStatusId);

                if (repair != null && purchaseOrderStatus != null)
                {
                    PurchaseOrder purchaseOrderAdd = new PurchaseOrder
                    {
                        repairTime = purchaseOrder.repairTime,
                        purchaseOrderDate = purchaseOrder.purchaseOrderDate,
                        repairId = purchaseOrder.repairId,
                        purchaseOrderStatusId = purchaseOrder.purchaseOrderStatusId,
                    };

                    await _appDbContext.PurchaseOrders.AddAsync(purchaseOrderAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Purchase Order not found
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 500; // Internal server error
            }
        }

        //Update purchaseOrder
        public async Task<int> UpdatePurchaseOrderAsync(int purchaseOrderId, PurchaseOrderViewModal purchaseOrder)
        {
            try
            {
                // Find the object in the db 
                PurchaseOrder attemptToFindInDb = await _appDbContext.PurchaseOrders.FirstOrDefaultAsync(x => x.purchaseOrderId == purchaseOrderId);

                if (attemptToFindInDb == null)
                {
                    return 404; // PurchaseOrder not found
                }

                attemptToFindInDb.repairTime = purchaseOrder.repairTime;
                attemptToFindInDb.purchaseOrderDate = purchaseOrder.purchaseOrderDate;

                Repair repair = await _appDbContext.Repairs.FindAsync(purchaseOrder.repairId);
                PurchaseOrderStatus purchaseOrderStatus = await _appDbContext.PurchaseOrderStatuses.FindAsync(purchaseOrder.purchaseOrderStatusId);

                if (repair != null && purchaseOrderStatus != null)
                {
                    attemptToFindInDb.repairId = purchaseOrder.repairId;
                    attemptToFindInDb.purchaseOrderStatusId = purchaseOrder.purchaseOrderStatusId;

                    _appDbContext.PurchaseOrders.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Purchase Order not found
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete purchaseOrder
        public async Task<int> DeletePurchaseOrderAsync(int purchaseOrderId)
        {
            // Find the object in the db 
            PurchaseOrder purchaseOrderToDelete = await _appDbContext.PurchaseOrders.FirstOrDefaultAsync(x => x.purchaseOrderId == purchaseOrderId);

            if (purchaseOrderToDelete == null)
            {
                return 404; // PurchaseOrder not found
            }

            _appDbContext.PurchaseOrders.Remove(purchaseOrderToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search purchaseOrder
        public async Task<List<PurchaseOrder>> SearchPurchaseOrderAsync(string searchString)
        {
            List<PurchaseOrder> purchaseOrderes = await _appDbContext.PurchaseOrders
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog).ThenInclude(errorLog => errorLog.errorCode)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.repairStatus)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
                //.Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.client)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.employee).ThenInclude(employee => employee.user)
                .Include(c => c.repair).ThenInclude(purchaseOrder => purchaseOrder.employee).ThenInclude(employee => employee.employeeType)
                .Include(c => c.purchaseOrderStatus)
                .Where(x => x.repair.errorLog.assignedPrinter.serialNumber.Contains(searchString)).ToListAsync();

            return purchaseOrderes;
        }
    }
}
