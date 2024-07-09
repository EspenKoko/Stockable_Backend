using Stockable_Backend.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class TransitPrinterRepository : ITransitPrinterRepository
    {
        private readonly AppDbContext _appDbContext;

        public TransitPrinterRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //TransitPrinter
        //Get all TransitPrinters
        public async Task<TransitPrinter[]> GetAllTransitPrintersAsync()
        {
            IQueryable<TransitPrinter> query = _appDbContext.TransitPrinters;
            return await query.ToArrayAsync();
        }

        //Get TransitPrinter
        public async Task<TransitPrinter> GetTransitPrinterAsync(int TransitPrinterId)
        {
            TransitPrinter TransitPrinter = await _appDbContext.TransitPrinters
                .FirstOrDefaultAsync(x => x.transitPrinterId == TransitPrinterId);

            return TransitPrinter;
        }

        // Create TransitPrinter
        public async Task<int> AddTransitPrinterAsync(TransitPrinterViewModal TransitPrinter)
        {
            try
            {
                    TransitPrinter TransitPrinterAdd = new TransitPrinter
                    {
                        technicianId = TransitPrinter.technicianId,
                        date = TransitPrinter.date,
                        assignedPrinterId = TransitPrinter.assignedPrinterId,
                        errorLogId = TransitPrinter.errorLogId
                    };

                    await _appDbContext.TransitPrinters.AddAsync(TransitPrinterAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200;
                }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update TransitPrinter
        public async Task<int> UpdateTransitPrinterAsync(int TransitPrinterId, TransitPrinterViewModal TransitPrinter)
        {
            try
            {
                TransitPrinter existingTransitPrinter = await _appDbContext.TransitPrinters.FindAsync(TransitPrinterId);
                if (existingTransitPrinter == null)
                {
                    return 404;
                }

                existingTransitPrinter.technicianId = TransitPrinter.technicianId;
                existingTransitPrinter.date = TransitPrinter.date;
                existingTransitPrinter.assignedPrinterId = TransitPrinter.assignedPrinterId;
                existingTransitPrinter.errorLogId = TransitPrinter.errorLogId;

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete TransitPrinter
        public async Task<int> DeleteTransitPrinterAsync(int TransitPrinterId)
        {
            TransitPrinter attemptToFindInDb = await _appDbContext.TransitPrinters.FindAsync(TransitPrinterId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.TransitPrinters.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search TransitPrinter
        public async Task<List<TransitPrinter>> SearchTransitPrinterAsync(string searchString)
        {
            List<TransitPrinter> TransitPrinters = await _appDbContext.TransitPrinters
                .Where(x => x.technicianId.Contains(searchString)).ToListAsync();

            return TransitPrinters;
        }
    }
}
