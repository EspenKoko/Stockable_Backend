using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;
using System.Collections.Generic;

namespace Stockable_Backend.Repository
{
    public class AssignedTechnicianRepository : IAssignedTechnicianRepository
    {
        private readonly AppDbContext _appDbContext;

        public AssignedTechnicianRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //AssignedTechnician
        //Get all assignedTechnician
        public async Task<AssignedTechnician[]> GetAllAssignedTechnicianAsync()
        {
            IQueryable<AssignedTechnician> query = _appDbContext.AssignedTechnicians
            .Include(b => b.employee.employeeType)
            .Include(b => b.employee.user)
            .Include(b => b.errorLog.errorCode)
            .Include(b => b.errorLog.errorLogStatus)
            .Include(b => b.errorLog.clientUser.user)
            .Include(b => b.errorLog.clientUser.branch.city.province)
            //.Include(b => b.errorLog.assignedPrinter.printerModel)
            .Include(b => b.errorLog.assignedPrinter.printerStatus)
            .Include(b => b.errorLog.assignedPrinter.client);

            return await query.ToArrayAsync();
        }

        //Get assignedTechnician
        public async Task<List<AssignedTechnician>> GetAssignedTechnicianAsync(int errorLogId)
        {
            List<AssignedTechnician> assignedTechnician = await _appDbContext.AssignedTechnicians
                .Include(b => b.employee.employeeType)
                .Include(b => b.employee.user)
                .Include(b => b.errorLog.errorCode)
                .Include(b => b.errorLog.errorLogStatus)
                .Include(b => b.errorLog.clientUser.user)
                .Include(b => b.errorLog.clientUser.branch.city.province)
                //.Include(b => b.errorLog.assignedPrinter.printerModel)
                .Include(b => b.errorLog.assignedPrinter.printerStatus)
                .Include(b => b.errorLog.assignedPrinter.client)
                .Where(x => x.errorLogId == errorLogId).ToListAsync();

            return assignedTechnician;
        }

        //Create assignedTechnician
        public async Task<int> AddAssignedTechnicianAsync(AssignedTechnicianViewModal assignedTechnician)
        {
            try
            {
                Employee employee = await _appDbContext.Employees.FindAsync(assignedTechnician.employeeId);
                ErrorLog errorlog = await _appDbContext.ErrorLogs.FindAsync(assignedTechnician.errorLogId);

                if (employee != null && errorlog != null)
                {
                    AssignedTechnician assignedTechnicianAdd = new AssignedTechnician
                    {
                        //isAssigned = assignedTechnician.isAssigned,
                        employeeId = assignedTechnician.employeeId,
                        errorLogId = assignedTechnician.errorLogId,

                    };

                    await _appDbContext.AssignedTechnicians.AddAsync(assignedTechnicianAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Employee or errorlog not found
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 500; // Internal server error
            }
        }

        //Update assignedTechnician
        public async Task<int> UpdateAssignedTechnicianAsync(int errorLogId, AssignedTechnicianViewModal assignedTechnician)
        {
            try
            {
                // Find the object in the db 
                AssignedTechnician attemptToFindInDb = await _appDbContext.AssignedTechnicians.FirstOrDefaultAsync(x => x.errorLogId == errorLogId);

                if (attemptToFindInDb == null)
                {
                    return 404; // AssignedTechnician not found
                }

                //attemptToFindInDb.isAssigned = assignedTechnician.isAssigned;

                Employee employee = await _appDbContext.Employees.FindAsync(assignedTechnician.employeeId);
                ErrorLog errorlog = await _appDbContext.ErrorLogs.FindAsync(assignedTechnician.errorLogId);

                if (employee != null && errorlog != null)
                {
                    //attemptToFindInDb.isAssigned = assignedTechnician.isAssigned;
                    attemptToFindInDb.employeeId = assignedTechnician.employeeId;
                    attemptToFindInDb.errorLogId = assignedTechnician.errorLogId;

                    _appDbContext.AssignedTechnicians.Update(attemptToFindInDb);
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

        //Delete assignedTechnician
        public async Task<int> DeleteAssignedTechnicianAsync(int errorLogId)
        {
            // Find the object in the db 
            AssignedTechnician assignedTechnicianToDelete = await _appDbContext.AssignedTechnicians.FirstOrDefaultAsync(x => x.errorLogId == errorLogId);

            if (assignedTechnicianToDelete == null)
            {
                return 404; // AssignedTechnician not found
            }

            _appDbContext.AssignedTechnicians.Remove(assignedTechnicianToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search assignedTechnician
        public async Task<List<AssignedTechnician>> SearchAssignedTechnicianAsync(string searchString)
        {
            List<AssignedTechnician> assignedTechnicianes = await _appDbContext.AssignedTechnicians
                .Include(b => b.employee.employeeType)
                .Include(b => b.employee.user)
                .Include(b => b.errorLog.errorCode)
                .Include(b => b.errorLog.errorLogStatus)
                .Include(b => b.errorLog.clientUser.user)
                .Include(b => b.errorLog.clientUser.branch.city.province)
                //.Include(b => b.errorLog.assignedPrinter.printerModel)
                .Include(b => b.errorLog.assignedPrinter.printerStatus)
                .Include(b => b.errorLog.assignedPrinter.client)
                .Where(x => x.employee.user.userFirstName.Contains(searchString) || x.employee.user.userLastName.Contains(searchString)).ToListAsync();

            return assignedTechnicianes;
        }
    }
}
