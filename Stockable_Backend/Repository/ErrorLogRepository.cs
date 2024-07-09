using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly AppDbContext _appDbContext;

        public ErrorLogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ErrorLog
        //Get all errorlogs
        public async Task<ErrorLog[]> GetAllErrorLogAsync()
        {
            IQueryable<ErrorLog> query = _appDbContext.ErrorLogs
            .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.client)
            .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.user)
            .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
            .Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
            //.Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
            .Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.client)
            .Include(b => b.errorCode)
            //.Include(b => b.assignedTechnician.employee.user)
            //.Include(b => b.assignedTechnician.employee.employeeType)
            .Include(b => b.errorLogStatus);

            return await query.ToArrayAsync();
        }

        //Get errorLog
        public async Task<ErrorLog> GetErrorLogAsync(int errorLogId)
        {
            ErrorLog errorLog = await _appDbContext.ErrorLogs
                .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.user)
                .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
                .Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
                //.Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
                .Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.client)
                .Include(b => b.errorCode)
                //.Include(b => b.assignedTechnician.employee.user)
                //.Include(b => b.assignedTechnician.employee.employeeType)
                .Include(b => b.errorLogStatus)
                .FirstOrDefaultAsync(x => x.errorLogId == errorLogId);

            return errorLog;
        }

        //Create errorLog
        public async Task<int> AddErrorLogAsync(ErrorLogViewModal errorLog)
        {
            try
            {
                ClientUser clientUser = await _appDbContext.ClientUsers.FindAsync(errorLog.clientUserId);
                ErrorLogStatus errorLogStatus = await _appDbContext.ErrorLogStatuses.FindAsync(errorLog.errorLogStatusId);
                AssignedPrinter assignedPrinter = await _appDbContext.AssignedPrinters.FindAsync(errorLog.assignedPrinterId);
                ErrorCode errorCode = await _appDbContext.ErrorCodes.FindAsync(errorLog.errorCodeId);
                //AssignedTechnician assignedTechnician = await _appDbContext.AssignedTechnicians.FindAsync(errorLog.assignedTechnicianId);

                if (clientUser != null && errorLogStatus != null && assignedPrinter != null && errorCode != null /*&& assignedTechnician != null*/)
                {
                    ErrorLog errorLogAdd = new ErrorLog
                    {
                        errorLogDate = errorLog.errorlogDate,
                        errorLogDescription = errorLog.errorLogDescription,
                        clientUserId = errorLog.clientUserId,
                        errorLogStatusId = errorLog.errorLogStatusId,
                        assignedPrinterId = errorLog.assignedPrinterId,
                        errorCodeId = errorLog.errorCodeId,
                        //assignedTechnicianId = errorLog.assignedTechnicianId,
                    };

                    await _appDbContext.ErrorLogs.AddAsync(errorLogAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Client or status not found
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 500; // Internal server error
            }
        }

        //Update errorLog
        public async Task<int> UpdateErrorLogAsync(int errorLogId, ErrorLogViewModal errorLog)
        {
            try
            {
                // Find the object in the db 
                ErrorLog attemptToFindInDb = await _appDbContext.ErrorLogs.FirstOrDefaultAsync(x => x.errorLogId == errorLogId);

                if (attemptToFindInDb == null)
                {
                    return 404; // ErrorLog not found
                }

                attemptToFindInDb.errorLogDate = errorLog.errorlogDate;

                ClientUser clientUser = await _appDbContext.ClientUsers.FindAsync(errorLog.clientUserId);
                ErrorLogStatus errorLogStatus = await _appDbContext.ErrorLogStatuses.FindAsync(errorLog.errorLogStatusId);
                AssignedPrinter assignedPrinter = await _appDbContext.AssignedPrinters.FindAsync(errorLog.assignedPrinterId);
                ErrorCode errorCode = await _appDbContext.ErrorCodes.FindAsync(errorLog.errorCodeId);
                //AssignedTechnician assignedTechnician = await _appDbContext.AssignedTechnicians.FindAsync(errorLog.assignedTechnicianId);

                if (clientUser != null && errorLogStatus != null && assignedPrinter != null && errorCode != null /*&& assignedTechnician != null*/)
                {
                    attemptToFindInDb.errorLogDate = errorLog.errorlogDate;
                    attemptToFindInDb.errorLogDescription = errorLog.errorLogDescription;
                    attemptToFindInDb.clientUserId = errorLog.clientUserId;
                    attemptToFindInDb.errorLogStatusId = errorLog.errorLogStatusId;
                    attemptToFindInDb.assignedPrinterId = errorLog.assignedPrinterId;
                    attemptToFindInDb.errorCodeId = errorLog.errorCodeId;
                    //attemptToFindInDb.assignedTechnicianId = errorLog.assignedTechnicianId;

                    _appDbContext.ErrorLogs.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid client or status
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete errorLog
        public async Task<int> DeleteErrorLogAsync(int errorLogId)
        {
            // Find the object in the db 
            ErrorLog errorLogToDelete = await _appDbContext.ErrorLogs.FirstOrDefaultAsync(x => x.errorLogId == errorLogId);

            if (errorLogToDelete == null)
            {
                return 404; // ErrorLog not found
            }

            _appDbContext.ErrorLogs.Remove(errorLogToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search errorLog
        public async Task<List<ErrorLog>> SearchErrorLogAsync(string searchString)
        {
            List<ErrorLog> errorLoges = await _appDbContext.ErrorLogs.Include(b => b.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.user)
                .Include(b => b.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
                .Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
                //.Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
                .Include(b => b.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.client)
                .Include(b => b.errorCode)
                //.Include(b => b.assignedTechnician.employee.user)
                //.Include(b => b.assignedTechnician.employee.employeeType)
                .Include(b => b.errorLogStatus)
                .Where(x => x.assignedPrinter.serialNumber.Contains(searchString)).ToListAsync();

            return errorLoges;
        }
    }
}
