using Microsoft.EntityFrameworkCore;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Repository
{
    public class AssignedPrinterRepository : IAssignedPrinterRepository
    {
        private readonly AppDbContext _appDbContext;

        public AssignedPrinterRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Printer
        //Get all Printers
        public async Task<AssignedPrinter[]> GetAllPrinterAsync()
        {
            IQueryable<AssignedPrinter> query = _appDbContext.AssignedPrinters
                //.Include(c => c.printerModel)
                .Include(c => c.printerStatus)
                .Include(c => c.client)
                .Include(c => c.client);

            return await query.ToArrayAsync();
        }

        //Get Printer
        public async Task<AssignedPrinter> GetPrinterAsync(int printerId)
        {
            AssignedPrinter printer = await _appDbContext.AssignedPrinters
                //.Include(c => c.printerModel)
                .Include(c => c.printerStatus)
                .Include(c => c.client)
                .Include(c => c.client)
                .FirstOrDefaultAsync(x => x.assignedPrinterId == printerId);

            return printer;

        }


        // Create printer
        public async Task<int> AddPrinterAsync(AssignedPrinterViewModal printer)
        {
            try
            {
                AssignedPrinter printerAdd = new AssignedPrinter();
                printerAdd.serialNumber = printer.serialNumber;
                printerAdd.printerModel = printer.printerModel;

                //PrinterModel printerModel = await _appDbContext.PrinterModels.FindAsync(printer.printerModelId);
                PrinterStatus printerStatus = await _appDbContext.PrinterStatuses.FindAsync(printer.printerStatusId);
                Client client = await _appDbContext.Clients.FindAsync(printer.clientId);

                if (/*printerModel == null || */printerStatus == null || client == null)
                {
                    return 404;
                }

                //printerAdd.printerModelId = printer.printerModelId;
                printerAdd.printerStatusId = printer.printerStatusId;
                printerAdd.clientId = printer.clientId;


                await _appDbContext.AssignedPrinters.AddAsync(printerAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        // Update printer
        public async Task<int> UpdatePrinterAsync(int printerId, AssignedPrinterViewModal printer)
        {
            try
            {
                AssignedPrinter attemptToFindInDb = await _appDbContext.AssignedPrinters.FindAsync(printerId);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                attemptToFindInDb.serialNumber = printer.serialNumber;
                attemptToFindInDb.printerModel = printer.printerModel;


                //PrinterModel printerModel = await _appDbContext.PrinterModels.FindAsync(printer.printerModelId);
                PrinterStatus printerStatus = await _appDbContext.PrinterStatuses.FindAsync(printer.printerStatusId);

                if (/*printerModel == null || */printerStatus == null )
                {
                    return 501;
                }

                //attemptToFindInDb.printerModelId = printer.printerModelId;
                attemptToFindInDb.printerStatusId = printer.printerStatusId;

                _appDbContext.AssignedPrinters.Update(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        // Delete printer
        public async Task<int> DeletePrinterAsync(int printerId)
        {
            AssignedPrinter printerToDelete = await _appDbContext.AssignedPrinters.FindAsync(printerId);

            if (printerToDelete == null)
            {
                return 404;
            }

            _appDbContext.AssignedPrinters.Remove(printerToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search printer
        public async Task<List<AssignedPrinter>> SearchPrinterAsync(string searchString)
        {
            List<AssignedPrinter> printers = await _appDbContext.AssignedPrinters
                //.Include(c => c.printerModel)
                .Include(c => c.printerStatus)
                .Include(c => c.client)

                .Where(x => x.serialNumber.Contains(searchString)).ToListAsync();

            return printers;
        }

    }
}
