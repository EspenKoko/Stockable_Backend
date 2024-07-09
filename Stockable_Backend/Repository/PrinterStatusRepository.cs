using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class PrinterStatusRepository : IPrinterStatusRepository
    {
        private readonly AppDbContext _appDbContext;

        public PrinterStatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //PrinterStatus
        //Get PrinterStatus
        public async Task<PrinterStatus> GetPrinterStatusAsync(int printerStatusId)
        {
            PrinterStatus printerStatus = await _appDbContext.PrinterStatuses.Where(x => x.printerStatusId == printerStatusId).FirstOrDefaultAsync();

            return printerStatus;
        }

        //Get all PrinterStatuses
        public async Task<PrinterStatus[]> GetAllPrinterStatusAsync()
        {
            IQueryable<PrinterStatus> query = _appDbContext.PrinterStatuses;
            return await query.ToArrayAsync();
        }

        // Create PrinterStatus
        public async Task<int> AddPrinterStatusAsync(PrinterStatusViewModal printerStatus)
        {
            try
            {
                PrinterStatus printerStatusAdd = new PrinterStatus
                {
                    printerStatusName = printerStatus.printerStatusName
                };

                await _appDbContext.PrinterStatuses.AddAsync(printerStatusAdd);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 500;
            }

            return 200;
        }

        // Update PrinterStatus
        public async Task<int> UpdatePrinterStatusAsync(int printerStatusId, PrinterStatusViewModal printerStatus)
        {
            PrinterStatus attemptToFindInDb = await _appDbContext.PrinterStatuses.FindAsync(printerStatusId);

            if (attemptToFindInDb == null)
            {
                return 404;
            }

            attemptToFindInDb.printerStatusName = printerStatus.printerStatusName;
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Delete PrinterStatus
        public async Task<int> DeletePrinterStatusAsync(int printerStatusId)
        {
            PrinterStatus attemptToFindInDb = await _appDbContext.PrinterStatuses.FindAsync(printerStatusId);

            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.PrinterStatuses.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search PrinterStatus
        public async Task<List<PrinterStatus>> SearchPrinterStatusAsync(string searchString)
        {
            List<PrinterStatus> printerStatuses = await _appDbContext.PrinterStatuses
                .Where(x => x.printerStatusName.Contains(searchString))
                .ToListAsync();

            return printerStatuses;
        }
    }
}
