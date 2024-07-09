using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class RepairDiagnosticRepository : IRepairDiagnosticRepository
    {
        private readonly AppDbContext _appDbContext;

        public RepairDiagnosticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //RepairDiagnostics
        //Get all erepairDiagnostics
        public async Task<RepairDiagnostic[]> GetAllRepairDiagnosticsAsync()
        {
            IQueryable<RepairDiagnostic> query = _appDbContext.RepairDiagnostics
            .Include(c => c.diagnostics)
            .Include(c => c.repair).ThenInclude(repair => repair.repairStatus)
            .Include(c => c.repair).ThenInclude(repair => repair.employee.user)
            .Include(c => c.repair).ThenInclude(repair => repair.employee.employeeType)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorCode)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.branch.city.province)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
            .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus);
            //.Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel);
            

            return await query.ToArrayAsync();
        }

        //Get repairDiagnostics
        public async Task<RepairDiagnostic> GetRepairDiagnosticsAsync(int repairId)
        {
            RepairDiagnostic repairDiagnostics = await _appDbContext.RepairDiagnostics
                .Include(c => c.diagnostics)
                .Include(c => c.repair).ThenInclude(repair => repair.repairStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.employee.user)
                .Include(c => c.repair).ThenInclude(repair => repair.employee.employeeType)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorCode)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.branch.city.province)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus)
            //.Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel)
                .FirstOrDefaultAsync(x => x.repairId == repairId);

            return repairDiagnostics;
        }

        //Create repairDiagnostics
        public async Task<int> AddRepairDiagnosticsAsync(RepairDiagnosticViewModal repairDiagnostics)
        {
            try
            {
                Diagnostics diagnostics = await _appDbContext.Diagnostics.FindAsync(repairDiagnostics.diagnosticsId);
                Repair repair = await _appDbContext.Repairs.FindAsync(repairDiagnostics.repairId);


                if (diagnostics != null && repair != null)
                {
                    RepairDiagnostic repairDiagnosticsAdd = new RepairDiagnostic
                    {
                        diagnosticsId = repairDiagnostics.diagnosticsId,
                        repairId = repairDiagnostics.repairId,
                        isComplete = repairDiagnostics.isComplete
                    };

                    await _appDbContext.RepairDiagnostics.AddAsync(repairDiagnosticsAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Diagnostics or Repair not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while saving to the database: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                return 500; // Internal server error
            }
        }

        //Update repairDiagnostics
        public async Task<int> UpdateRepairDiagnosticsAsync(int repairId, RepairDiagnosticViewModal repairDiagnostics)
        {
            try
            {
                // Find the object in the db 
                RepairDiagnostic attemptToFindInDb = await _appDbContext.RepairDiagnostics.FirstOrDefaultAsync(x => x.repairId == repairId);

                if (attemptToFindInDb == null)
                {
                    return 404; // RepairDiagnostics not found
                }

                Diagnostics diagnostics = await _appDbContext.Diagnostics.FindAsync(repairDiagnostics.diagnosticsId);
                Repair repair = await _appDbContext.Repairs.FindAsync(repairDiagnostics.repairId);

                if (diagnostics != null && repair != null)
                {
                    attemptToFindInDb.diagnosticsId = repairDiagnostics.diagnosticsId;
                    attemptToFindInDb.repairId = repairDiagnostics.repairId;
                    attemptToFindInDb.isComplete = repairDiagnostics.isComplete;

                    _appDbContext.RepairDiagnostics.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid Diagnostics or Repair
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }


        //Delete repairDiagnostics
        public async Task<int> DeleteRepairDiagnosticsAsync(int repairId)
        {
            // Find the object in the db 
            RepairDiagnostic repairDiagnosticsToDelete = await _appDbContext.RepairDiagnostics.FirstOrDefaultAsync(x => x.repairId == repairId);

            if (repairDiagnosticsToDelete == null)
            {
                return 404; // RepairDiagnostics not found
            }

            _appDbContext.RepairDiagnostics.Remove(repairDiagnosticsToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search repairDiagnostics
        public async Task<List<RepairDiagnostic>> SearchRepairDiagnosticsAsync(string searchString)
        {
            List<RepairDiagnostic> repairDiagnosticses = await _appDbContext.RepairDiagnostics
                .Include(c => c.diagnostics)
                .Include(c => c.repair).ThenInclude(repair => repair.repairStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.employee.user)
                .Include(c => c.repair).ThenInclude(repair => repair.employee.employeeType)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorCode)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.errorLogStatus)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.branch.city.province)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.client)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.clientUser.user)
                .Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerStatus)
                //.Include(c => c.repair).ThenInclude(repair => repair.errorLog.assignedPrinter.printerModel)
                .Where(x => x.repair.errorLog.assignedPrinter.serialNumber.Contains(searchString) || x.repair.employee.user.userFirstName.Contains(searchString))
                .ToListAsync();

            return repairDiagnosticses;
        }
    }
}
