using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class RepairStatusRepository : IRepairStatusRepository
    {
        private readonly AppDbContext _appDbContext;

        public RepairStatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //RepairStatus
        //Get all repairStatuss
        public async Task<RepairStatus[]> GetAllRepairStatusAsync()
        {
            IQueryable<RepairStatus> query = _appDbContext.RepairStatuses;
            return await query.ToArrayAsync();
        }

        //Get repairStatus
        public async Task<RepairStatus> GetRepairStatusAsync(int repairStatusId)
        {
            RepairStatus repairStatus = await _appDbContext.RepairStatuses.FirstOrDefaultAsync(x => x.repairStatusId == repairStatusId);

            return repairStatus;
        }


        //Create repairStatus
        public async Task<int> AddRepairStatusAsync(RepairStatusViewModal repairStatus)
        {
            try
            {
                RepairStatus repairStatusAdd = new RepairStatus
                {
                    repairStatusName = repairStatus.repairStatusName
                };

                _appDbContext.RepairStatuses.Add(repairStatusAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal Server Error
            }
        }

        //Update repairStatus
        public async Task<int> UpdateRepairStatusAsync(int repairStatusId, RepairStatusViewModal repairStatus)
        {
            // Find the object in the db 
            RepairStatus attemptToFindInDb = await _appDbContext.RepairStatuses.FirstOrDefaultAsync(x => x.repairStatusId == repairStatusId);

            if (attemptToFindInDb == null)
            {
                return 404; // RepairStatus not found
            }

            attemptToFindInDb.repairStatusName = repairStatus.repairStatusName;

            _appDbContext.RepairStatuses.Update(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        //Delete RepairStatus
        public async Task<int> DeleteRepairStatusAsync(int repairStatusId)
        {
            // Find the object in the db 
            RepairStatus attemptToFindInDb = await _appDbContext.RepairStatuses.FirstOrDefaultAsync(x => x.repairStatusId == repairStatusId);

            if (attemptToFindInDb == null)
            {
                return 404; // RepairStatus not found
            }

            _appDbContext.RepairStatuses.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search RepairStatus   
        public async Task<List<RepairStatus>> SearchRepairStatusAsync(string searchString)
        {
            List<RepairStatus> repairStatuss = await _appDbContext.RepairStatuses.Where(x => x.repairStatusName.Contains(searchString)).ToListAsync();

            return repairStatuss;
        }
    }
}
