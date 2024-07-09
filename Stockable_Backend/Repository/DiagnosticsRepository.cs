using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class DiagnosticsRepository : IDiagnosticsRepository
    {
        private readonly AppDbContext _appDbContext;

        public DiagnosticsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Diagnostics
        //Get all diagnosticses
        public async Task<Diagnostics[]> GetAllDiagnosticsAsync()
        {
            IQueryable<Diagnostics> query = _appDbContext.Diagnostics
                //.Include(b => b.repair.errorLog.assignedPrinter.printerModel)
                .Include(b => b.repair.errorLog.assignedPrinter.printerStatus)
                .Include(b => b.repair.errorLog.assignedPrinter.client)
                .Include(b => b.repair.errorLog.clientUser.branch.city.province)
                .Include(b => b.repair.errorLog.clientUser.user)
                .Include(b => b.repair.errorLog.errorCode)
                .Include(b => b.repair.errorLog.errorLogStatus)
                .Include(b => b.repair.repairStatus)
                .Include(b => b.repair.employee.user)
                .Include(b => b.repair.employee.employeeType);

            return await query.ToArrayAsync();
        }

        //Get diagnostics
        public async Task<Diagnostics> GetDiagnosticsAsync(int diagnosticsId)
        {
            Diagnostics diagnostics = await _appDbContext.Diagnostics
                //.Include(b => b.repair.errorLog.assignedPrinter.printerModel)
                .Include(b => b.repair.errorLog.assignedPrinter.printerStatus)
                .Include(b => b.repair.errorLog.assignedPrinter.client)
                .Include(b => b.repair.errorLog.clientUser.branch.city.province)
                .Include(b => b.repair.errorLog.clientUser.user)
                .Include(b => b.repair.errorLog.errorCode)
                .Include(b => b.repair.errorLog.errorLogStatus)
                .Include(b => b.repair.repairStatus)
                .Include(b => b.repair.employee.user)
                .Include(b => b.repair.employee.employeeType)
                .FirstOrDefaultAsync(x => x.diagnosticsId == diagnosticsId);

            return diagnostics;
        }

        //Create diagnostics
        public async Task<int> AddDiagnosticsAsync(DiagnosticsViewModal diagnostics)
        {
            try
            {
                Repair repair = await _appDbContext.Repairs.FindAsync(diagnostics.repairId);

                if (repair != null)
                {
                    Diagnostics diagnosticsAdd = new Diagnostics
                    {
                        diagnosticComment = diagnostics.diagnosticComment,
                        rollerCheck = diagnostics.rollerCheck,
                        lcdScreenCheck = diagnostics.lcdScreenCheck,
                        powerSupplyCheck = diagnostics.powerSupplyCheck,
                        motherboardCheck = diagnostics.motherboardCheck,
                        hopperCheck = diagnostics.hopperCheck,
                        beltCheck = diagnostics.beltCheck,
                        ethernetPortCheck = diagnostics.ethernetPortCheck,
                        repairId = diagnostics.repairId,
                    };

                    _appDbContext.Diagnostics.Add(diagnosticsAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                return 404; // Client or city not found
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 500; // Internal server error
            }
        }

        //Update diagnostics
        public async Task<int> UpdateDiagnosticsAsync(int diagnosticsId, DiagnosticsViewModal diagnostics)
        {
            try
            {
                // Find the object in the db 
                Diagnostics attemptToFindInDb = await _appDbContext.Diagnostics.FirstOrDefaultAsync(x => x.diagnosticsId == diagnosticsId);

                if (attemptToFindInDb == null)
                {
                    return 404; // Diagnostics not found
                }

                attemptToFindInDb.rollerCheck = diagnostics.rollerCheck;
                attemptToFindInDb.lcdScreenCheck = diagnostics.lcdScreenCheck;
                attemptToFindInDb.powerSupplyCheck = diagnostics.powerSupplyCheck;
                attemptToFindInDb.motherboardCheck = diagnostics.motherboardCheck;
                attemptToFindInDb.hopperCheck = diagnostics.hopperCheck;
                attemptToFindInDb.beltCheck = diagnostics.beltCheck;
                attemptToFindInDb.ethernetPortCheck = diagnostics.ethernetPortCheck;

                Repair repair = await _appDbContext.Repairs.FindAsync(diagnostics.repairId);

                if (repair != null)
                {

                    attemptToFindInDb.repairId = diagnostics.repairId;

                    _appDbContext.Diagnostics.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid client or city
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete diagnostics
        public async Task<int> DeleteDiagnosticsAsync(int diagnosticsId)
        {
            // Find the object in the db 
            Diagnostics diagnosticsToDelete = await _appDbContext.Diagnostics.FirstOrDefaultAsync(x => x.diagnosticsId == diagnosticsId);

            if (diagnosticsToDelete == null)
            {
                return 404; // Diagnostics not found
            }

            _appDbContext.Diagnostics.Remove(diagnosticsToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search diagnostics
        public async Task<List<Diagnostics>> SearchDiagnosticsAsync(string searchString)
        {
            List<Diagnostics> diagnosticses = await _appDbContext.Diagnostics
                //.Include(b => b.repair.errorLog.assignedPrinter.printerModel)
                .Include(b => b.repair.errorLog.assignedPrinter.printerStatus)
                .Include(b => b.repair.errorLog.assignedPrinter.client)
                .Include(b => b.repair.errorLog.clientUser.branch.city.province)
                .Include(b => b.repair.errorLog.clientUser.user)
                .Include(b => b.repair.errorLog.errorCode)
                .Include(b => b.repair.errorLog.errorLogStatus)
                .Include(b => b.repair.repairStatus)
                .Include(b => b.repair.employee.user)
                .Include(b => b.repair.employee.employeeType)
                .Where(x => x.repair.errorLog.assignedPrinter.serialNumber.Contains(searchString)).ToListAsync();

            return diagnosticses;
        }
    }
}
