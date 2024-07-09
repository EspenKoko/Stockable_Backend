using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class RepairRepository : IRepairRepository
    {
        private readonly AppDbContext _appDbContext;

        public RepairRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Repair
        //Get all repairs
        public async Task<Repair[]> GetAllRepairAsync()
        {
            IQueryable<Repair> query = _appDbContext.Repairs
            .Include(c => c.repairStatus)
            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user)
            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.branch).ThenInclude(branch => branch.city).ThenInclude(city => city.province)
            //.Include(c => c.errorLog).ThenInclude(errorLog => errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
            .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorCode)
            .Include(c => c.employee).ThenInclude(employee => employee.employeeType)
            .Include(c => c.employee).ThenInclude(employee => employee.user);

            return await query.ToArrayAsync();
        }

        //Get a repair
        public async Task<Repair> GetRepairAsync(int repairId)
        {
            Repair repair = await _appDbContext.Repairs
                .Include(c => c.repairStatus)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.branch).ThenInclude(branch => branch.city).ThenInclude(city => city.province)
                //.Include(c => c.errorLog).ThenInclude(errorLog => errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorCode)
                .Include(c => c.employee).ThenInclude(employee => employee.employeeType)
                .Include(c => c.employee).ThenInclude(employee => employee.user)
                .FirstOrDefaultAsync(x => x.repairId == repairId);

            return repair;
        }

        //Create repair
        public async Task<int> AddRepairAsync(RepairViewModal repair)
        {
            try
            {
                ErrorLog errorLog = await _appDbContext.ErrorLogs.FindAsync(repair.errorLogId);
                RepairStatus repairStatus = await _appDbContext.RepairStatuses.FindAsync(repair.repairStatusId);
                Employee employee = await _appDbContext.Employees.FindAsync(repair.employeeId);

                if (errorLog != null && repairStatus != null && employee != null)
                {
                    Repair repairAdd = new Repair
                    {
                        errorLogId = repair.errorLogId,
                        repairStatusId = repair.repairStatusId,
                        employeeId = repair.employeeId
                    };

                    await _appDbContext.Repairs.AddAsync(repairAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // ErrorLog or RepairStatus or Printer or Employee not found
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update repair
        public async Task<int> UpdateRepairAsync(int repairId, RepairViewModal repair)
        {
            try
            {
                // Find the object in the db 
                Repair attemptToFindInDb = await _appDbContext.Repairs.FirstOrDefaultAsync(x => x.repairId == repairId);

                if (attemptToFindInDb == null)
                {
                    return 404; // Repair not found
                }

                // Check if the corresponding properties are present in the repair view model and update only those that are present
                if (repair.errorLogId != 0)
                {
                    ErrorLog errorLog = await _appDbContext.ErrorLogs.FindAsync(repair.errorLogId);
                    if (errorLog == null)
                    {
                        return 501; // Invalid ErrorLog
                    }
                    attemptToFindInDb.errorLogId = repair.errorLogId;
                }

                if (repair.repairStatusId != 0)
                {
                    RepairStatus repairStatus = await _appDbContext.RepairStatuses.FindAsync(repair.repairStatusId);
                    if (repairStatus == null)
                    {
                        return 501; // Invalid RepairStatus
                    }
                    attemptToFindInDb.repairStatusId = repair.repairStatusId;
                }

                if (repair.employeeId != 0)
                {
                    Employee employee = await _appDbContext.Employees.FindAsync(repair.employeeId);
                    if (employee == null)
                    {
                        return 501; // Invalid Employee
                    }
                    attemptToFindInDb.employeeId = repair.employeeId;
                }

                _appDbContext.Repairs.Update(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }
        }


        //Delete repair
        public async Task<int> DeleteRepairAsync(int repairId)
        {
            // Find the object in the db 
            Repair repairToDelete = await _appDbContext.Repairs.FirstOrDefaultAsync(x => x.repairId == repairId);

            if (repairToDelete == null)
            {
                return 404; // Repair not found
            }

            _appDbContext.Repairs.Remove(repairToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search repair
        public async Task<List<Repair>> SearchRepairAsync(string searchString)
        {
            List<Repair> repaires = await _appDbContext.Repairs
                .Include(c => c.repairStatus)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorLogStatus)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.user)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.clientUser).ThenInclude(clientUser => clientUser.branch).ThenInclude(branch => branch.city).ThenInclude(city => city.province)
                //.Include(c => c.errorLog).ThenInclude(errorLog => errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerModel)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.assignedPrinter).ThenInclude(assignedPrinter => assignedPrinter.printerStatus)
                .Include(c => c.errorLog).ThenInclude(errorLog => errorLog.errorCode)
                .Include(c => c.employee).ThenInclude(employee => employee.employeeType)
                .Include(c => c.employee).ThenInclude(employee => employee.user)
                .Where(x => x.errorLog.assignedPrinter.serialNumber.Contains(searchString) || x.repairStatus.repairStatusName.Contains(searchString))
                .ToListAsync();

            return repaires;
        }
    }
}
