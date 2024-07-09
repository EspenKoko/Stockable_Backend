//using Microsoft.EntityFrameworkCore;
//using Stockable_Backend.Model;
//using Stockable_Backend.Repository.IRepositories;
//using Stockable_Backend.ViewModel;

//namespace Stockable_Backend.Repository
//{
//    public class ErrorLogErrorCodesRepository : IErrorLogErrorCodesRepository
//    {
//        private readonly AppDbContext _appDbContext;

//        public ErrorLogErrorCodesRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }

//        //ErrorLogErrorCodes
//        //Get all errorLogErrorCodeses
//        public async Task<ErrorLogErrorCodes[]> GetAllErrorLogErrorCodesAsync()
//        {
//            IQueryable<ErrorLogErrorCodes> query = _appDbContext.ErrorLogErrorCodes
//            .Include(c => c.errorCode)
//            .Include(c => c.printer).ThenInclude(printer => printer.printerStatus)
//            .Include(c => c.printer).ThenInclude(printer => printer.printerModel)
//            .Include(c => c.printer).ThenInclude(printer => printer.client)
//            //.Include(c => c.printer).ThenInclude(printer => printer.branch).ThenInclude(branch => branch.city).ThenInclude(city => city.province)
//            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
//            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
//            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user);

//            return await query.ToArrayAsync();
//        }

//        //Get errorLogErrorCodes
//        public async Task<ErrorLogErrorCodes> GetErrorLogErrorCodesAsync(int printerId)
//        {
//            ErrorLogErrorCodes errorLogErrorCodes = await _appDbContext.ErrorLogErrorCodes
//                .Include(c => c.errorCode)
//                .Include(c => c.printer).ThenInclude(printer => printer.printerStatus)
//                .Include(c => c.printer).ThenInclude(printer => printer.printerModel)
//                .Include(c => c.printer).ThenInclude(printer => printer.client)
//                //.Include(c => c.printer).ThenInclude(printer => printer.branch).ThenInclude(branch => branch.city).ThenInclude(city => city.province)
//                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
//                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
//                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user)
//                .FirstOrDefaultAsync(x => x.printerId == printerId);

//            return errorLogErrorCodes;
//        }

//        //Create errorLogErrorCodes
//        public async Task<int> AddErrorLogErrorCodesAsync(ErrorLogErrorCodesViewModal errorLogErrorCodes)
//        {
//            try
//            {
//                ErrorCode errorCode = await _appDbContext.ErrorCodes.FindAsync(errorLogErrorCodes.errorCodeId);
//                ErrorLog errorLog = await _appDbContext.ErrorLogs.FindAsync(errorLogErrorCodes.errorLogId);
//                AssignedPrinter printer = await _appDbContext.Printers.FindAsync(errorLogErrorCodes.printerId);


//                if (errorCode != null && errorLog != null && printer != null)
//                {
//                    ErrorLogErrorCodes errorLogErrorCodesAdd = new ErrorLogErrorCodes
//                    {
//                        errorCodeId = errorLogErrorCodes.errorCodeId,
//                        errorLogId = errorLogErrorCodes.errorLogId,
//                        printerId = errorLogErrorCodes.printerId
//                    };

//                    await _appDbContext.ErrorLogErrorCodes.AddAsync(errorLogErrorCodesAdd);
//                    await _appDbContext.SaveChangesAsync();

//                    return 200; // Success
//                }

//                return 404; // ErrorCode or ErrorLog or Printer not found
//            }
//            catch (Exception ex)
//            {
//                // Log the exception details for better error diagnosis
//                Console.WriteLine($"Error occurred while saving to the database: {ex.Message}");
//                Console.WriteLine($"Stack trace: {ex.StackTrace}");

//                return 500; // Internal server error
//            }
//        }

//        //Update errorLogErrorCodes
//        public async Task<int> UpdateErrorLogErrorCodesAsync(int printerId, ErrorLogErrorCodesViewModal errorLogErrorCodes)
//        {
//            try
//            {
//                // Find the object in the db 
//                ErrorLogErrorCodes attemptToFindInDb = await _appDbContext.ErrorLogErrorCodes.FirstOrDefaultAsync(x => x.printerId == printerId);

//                if (attemptToFindInDb == null)
//                {
//                    return 404; // ErrorLogErrorCodes not found
//                }

//                ErrorCode errorCode = await _appDbContext.ErrorCodes.FindAsync(errorLogErrorCodes.errorCodeId);
//                ErrorLog errorLog = await _appDbContext.ErrorLogs.FindAsync(errorLogErrorCodes.errorLogId);
//                AssignedPrinter printer = await _appDbContext.Printers.FindAsync(errorLogErrorCodes.printerId);

//                if (errorCode != null && errorLog != null && printer != null)
//                {
//                    attemptToFindInDb.errorCodeId = errorLogErrorCodes.errorCodeId;
//                    attemptToFindInDb.errorLogId = errorLogErrorCodes.errorLogId;
//                    attemptToFindInDb.printerId = errorLogErrorCodes.printerId;

//                    _appDbContext.ErrorLogErrorCodes.Update(attemptToFindInDb);
//                    await _appDbContext.SaveChangesAsync();

//                    return 200; // Success
//                }
//                else
//                {
//                    return 501; // Invalid ErrorCode or ErrorLog or Printer
//                }
//            }
//            catch (Exception)
//            {
//                return 500; // Internal server error
//            }

//        }

//        //Delete errorLogErrorCodes
//        public async Task<int> DeleteErrorLogErrorCodesAsync(int printerId)
//        {
//            // Find the object in the db 
//            ErrorLogErrorCodes errorLogErrorCodesToDelete = await _appDbContext.ErrorLogErrorCodes.FirstOrDefaultAsync(x => x.printerId == printerId);

//            if (errorLogErrorCodesToDelete == null)
//            {
//                return 404; // ErrorLogErrorCodes not found
//            }

//            _appDbContext.ErrorLogErrorCodes.Remove(errorLogErrorCodesToDelete);
//            await _appDbContext.SaveChangesAsync();

//            return 200; // Success
//        }

//        //Search errorLogErrorCodes
//        public async Task<List<ErrorLogErrorCodes>> SearchErrorLogErrorCodesAsync(string searchString)
//        {
//            List<ErrorLogErrorCodes> errorLogErrorCodeses = await _appDbContext.ErrorLogErrorCodes
//                .Include(c => c.errorCode)
//                .Include(c => c.printer).ThenInclude(printer => printer.printerStatus)
//                .Include(c => c.printer).ThenInclude(printer => printer.printerModel)
//                .Include(c => c.printer).ThenInclude(printer => printer.client)
//                //.Include(c => c.printer).ThenInclude(printer => printer.branch).ThenInclude(branch => branch.city).ThenInclude(city => city.province)
//                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
//                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
//                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user)
//                .Where(x => x.printer.serialNumber.Contains(searchString)).ToListAsync();

//            return errorLogErrorCodeses;
//        }
//    }
//}
