using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _appDbContext;

        public BranchRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Branch
        //Get all branches
        public async Task<Branch[]> GetAllBranchAsync()
        {
            IQueryable<Branch> query = _appDbContext.Branches
                .Include(b => b.client)
                .Include(b => b.assignedPrinter.printerStatus)
                //.Include(b => b.assignedPrinter.printerModel)
                .Include(b => b.assignedPrinter.client)
                .Include(b => b.city)
                .ThenInclude(city => city.province);

            return await query.ToArrayAsync();
        }

        //Get branch
        public async Task<Branch> GetBranchAsync(int branchId)
        {
            Branch branch = await _appDbContext.Branches
                .Include(b => b.client)
                .Include(b => b.assignedPrinter.printerStatus)
                //.Include(b => b.assignedPrinter.printerModel)
                .Include(b => b.assignedPrinter.client)
                .Include(b => b.city)
                .ThenInclude(city => city.province)
                .FirstOrDefaultAsync(x => x.branchId == branchId);

            return branch;
        }

        //Create branch
        public async Task<int> AddBranchAsync(BranchViewModal branch)
        {
            try
            {
                Client client = await _appDbContext.Clients.FindAsync(branch.clientId);
                City city = await _appDbContext.Cities.FindAsync(branch.cityId);
                //AssignedPrinter assignedPrinter = await _appDbContext.AssignedPrinters.FindAsync(branch.assignedPrinterId);

                if (client != null && city != null /*&& assignedPrinter != null*/)
                {
                    Branch branchAdd = new Branch
                    {
                        branchName = branch.branchName,
                        branchCode = branch.branchCode,
                        clientId = branch.clientId,
                        cityId = branch.cityId,
                        assignedPrinterId = null,
                    };

                    await _appDbContext.Branches.AddAsync(branchAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Client or city not found
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        // Update branch
        public async Task<int> UpdateBranchAsync(int branchId, BranchViewModal branch)
        {
            try
            {
                // Find the object in the db 
                Branch attemptToFindInDb = await _appDbContext.Branches.FirstOrDefaultAsync(x => x.branchId == branchId);

                if (attemptToFindInDb == null)
                {
                    return 404; // Branch not found
                }

                attemptToFindInDb.branchName = branch.branchName;
                attemptToFindInDb.branchCode = branch.branchCode;

                Client client = await _appDbContext.Clients.FindAsync(branch.clientId);
                City city = await _appDbContext.Cities.FindAsync(branch.cityId);

                if (client != null && city != null)
                {
                    if (branch.assignedPrinterId != null)
                    {
                        AssignedPrinter assignedPrinter = await _appDbContext.AssignedPrinters.FindAsync(branch.assignedPrinterId);
                        if (assignedPrinter == null)
                        {
                            return 501; // Invalid assigned printer
                        }
                    }

                    attemptToFindInDb.cityId = branch.cityId;
                    attemptToFindInDb.clientId = branch.clientId;
                    attemptToFindInDb.assignedPrinterId = branch.assignedPrinterId;

                    _appDbContext.Branches.Update(attemptToFindInDb);
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

        //Delete branch
        public async Task<int> DeleteBranchAsync(int branchId)
        {
            // Find the object in the db 
            Branch branchToDelete = await _appDbContext.Branches.FirstOrDefaultAsync(x => x.branchId == branchId);

            if (branchToDelete == null)
            {
                return 404; // Branch not found
            }

            _appDbContext.Branches.Remove(branchToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search branch
        public async Task<List<Branch>> SearchBranchAsync(string searchString)
        {
            List<Branch> branches = await _appDbContext.Branches
                .Include(b => b.client)
                .Include(b => b.assignedPrinter.printerStatus)
                //.Include(b => b.assignedPrinter.printerModel)
                .Include(b => b.assignedPrinter.client)
                .Include(b => b.city)
                .ThenInclude(city => city.province)
                .Where(x => x.branchName.Contains(searchString)).ToListAsync();

            return branches;
        }
    }
}
