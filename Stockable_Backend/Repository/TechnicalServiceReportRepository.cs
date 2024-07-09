using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class TechnicalServiceReportRepository : ITechnicalServiceReportRepository
    {
        private readonly AppDbContext _appDbContext;

        public TechnicalServiceReportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //TechnicalServiceReport
        //Get all technicalServiceReports
        public async Task<TechnicalServiceReport[]> GetAllTechnicalServiceReportAsync()
        {
            IQueryable<TechnicalServiceReport> query = _appDbContext.TechnicalServiceReports
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.errorCode)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.branch)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.employee.employeeType)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.employee.user)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus)
            .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.client);
            //.Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel);

            return await query.ToArrayAsync();
        }

        //Get technicalServiceReport
        public async Task<TechnicalServiceReport> GetTechnicalServiceReportAsync(int technicalServiceReportId)
        {
            TechnicalServiceReport technicalServiceReport = await _appDbContext.TechnicalServiceReports
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.errorCode)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.branch)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.employee.employeeType)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.employee.user)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.client)
                //.Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel)
                .FirstOrDefaultAsync(x => x.technicalServiceReportId == technicalServiceReportId);

            return technicalServiceReport;
        }

        //Create technicalServiceReport
        public async Task<int> AddTechnicalServiceReportAsync(TechnicalServiceReportViewModal technicalServiceReport)
        {
            try
            {
                PurchaseOrder purchaseOrder = await _appDbContext.PurchaseOrders.FindAsync(technicalServiceReport.purchaseOrderId);

                if (purchaseOrder != null)
                {
                    TechnicalServiceReport technicalServiceReportAdd = new TechnicalServiceReport
                    {
                        repairsDone = technicalServiceReport.repairsDone,
                        //timeElapst = technicalServiceReport.timeElapst,
                        //TSRDate = technicalServiceReport.TSRDate,
                        purchaseOrderId = technicalServiceReport.purchaseOrderId
                    };

                    await _appDbContext.TechnicalServiceReports.AddAsync(technicalServiceReportAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Repair Invoice not found
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 500; // Internal server error
            }
        }

        //Update technicalServiceReport
        public async Task<int> UpdateTechnicalServiceReportAsync(int technicalServiceReportId, TechnicalServiceReportViewModal technicalServiceReport)
        {
            try
            {
                // Find the object in the db 
                TechnicalServiceReport attemptToFindInDb = await _appDbContext.TechnicalServiceReports.FirstOrDefaultAsync(x => x.technicalServiceReportId == technicalServiceReportId);

                if (attemptToFindInDb == null)
                {
                    return 404; // TechnicalServiceReport not found
                }

                attemptToFindInDb.repairsDone = technicalServiceReport.repairsDone;
                //attemptToFindInDb.timeElapst = technicalServiceReport.timeElapst;
                //attemptToFindInDb.TSRDate = technicalServiceReport.TSRDate;

                PurchaseOrder purchaseOrder = await _appDbContext.PurchaseOrders.FindAsync(technicalServiceReport.purchaseOrderId);

                if (purchaseOrder != null)
                {
                    attemptToFindInDb.purchaseOrderId = technicalServiceReport.purchaseOrderId;

                    _appDbContext.TechnicalServiceReports.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid Repair Invoice
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete technicalServiceReport
        public async Task<int> DeleteTechnicalServiceReportAsync(int technicalServiceReportId)
        {
            // Find the object in the db 
            TechnicalServiceReport technicalServiceReportToDelete = await _appDbContext.TechnicalServiceReports.FirstOrDefaultAsync(x => x.technicalServiceReportId == technicalServiceReportId);

            if (technicalServiceReportToDelete == null)
            {
                return 404; // TechnicalServiceReport not found
            }

            _appDbContext.TechnicalServiceReports.Remove(technicalServiceReportToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search technicalServiceReport
        public async Task<List<TechnicalServiceReport>> SearchTechnicalServiceReportAsync(string searchString)
        {
            List<TechnicalServiceReport> technicalServiceReportes = await _appDbContext.TechnicalServiceReports
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.errorCode)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.branch)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.employee.employeeType)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.employee.user)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus)
                .Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.client)
                //.Include(c => c.purchaseOrder).ThenInclude(purchaseOrder => purchaseOrder.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel)
                .Where(x => x.purchaseOrder.repair.errorLog.assignedPrinter.serialNumber.Contains(searchString)).ToListAsync();

            return technicalServiceReportes;
        }
    }
}
