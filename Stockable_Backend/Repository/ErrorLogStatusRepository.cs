using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ErrorLogStatusRepository : IErrorLogStatusRepository
    {
        private readonly AppDbContext _appDbContext;

        public ErrorLogStatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ErrorLogStatus
        //Get all errorLogStatuses
        public async Task<ErrorLogStatus[]> GetAllErrorLogStatusAsync()
        {
            IQueryable<ErrorLogStatus> query = _appDbContext.ErrorLogStatuses;

            return await query.ToArrayAsync();
        }

        //Get errorLogStatus
        public async Task<ErrorLogStatus> GetErrorLogStatusAsync(int errorLogStatusId)
        {
            ErrorLogStatus errorLogStatus = await _appDbContext.ErrorLogStatuses.FirstOrDefaultAsync(x => x.errorLogStatusId == errorLogStatusId);

            return errorLogStatus;
        }

        //Create errorLogStatus
        public async Task<int> AddErrorLogStatusAsync(ErrorLogStatusViewModal errorLogStatus)
        {
            try
            {
                ErrorLogStatus errorLogStatusAdd = new ErrorLogStatus
                {
                    errorLogStatusName = errorLogStatus.errorLogStatusName,
                };

                await _appDbContext.ErrorLogStatuses.AddAsync(errorLogStatusAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update errorLogStatus
        public async Task<int> UpdateErrorLogStatusAsync(int errorLogStatusId, ErrorLogStatusViewModal errorLogStatus)
        {
            try
            {
                // Find the object in the db 
                ErrorLogStatus attemptToFindInDb = await _appDbContext.ErrorLogStatuses.FirstOrDefaultAsync(x => x.errorLogStatusId == errorLogStatusId);

                if (attemptToFindInDb == null)
                {
                    return 404; // ErrorLogStatus not found
                }

                attemptToFindInDb.errorLogStatusName = errorLogStatus.errorLogStatusName;

                _appDbContext.ErrorLogStatuses.Update(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete errorLogStatus
        public async Task<int> DeleteErrorLogStatusAsync(int errorLogStatusId)
        {
            // Find the object in the db 
            ErrorLogStatus errorLogStatusToDelete = await _appDbContext.ErrorLogStatuses.FirstOrDefaultAsync(x => x.errorLogStatusId == errorLogStatusId);

            if (errorLogStatusToDelete == null)
            {
                return 404; // ErrorLogStatus not found
            }

            _appDbContext.ErrorLogStatuses.Remove(errorLogStatusToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search errorLogStatus
        public async Task<List<ErrorLogStatus>> SearchErrorLogStatusAsync(string searchString)
        {
            List<ErrorLogStatus> errorLogStatuses = await _appDbContext.ErrorLogStatuses.Where(x => x.errorLogStatusName.Contains(searchString)).ToListAsync();

            return errorLogStatuses;
        }

    }
}
