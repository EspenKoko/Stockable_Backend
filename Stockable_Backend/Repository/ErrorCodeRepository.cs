using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ErrorCodeRepository : IErrorCodeRepository
    {
        private readonly AppDbContext _appDbContext;

        public ErrorCodeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ErrorCode
        //Get ErrorCode
        public async Task<ErrorCode> GetErrorCodeAsync(int errorCodeId)
        {
            ErrorCode errorCode = await _appDbContext.ErrorCodes.Where(x => x.errorCodeId == errorCodeId).FirstOrDefaultAsync();

            return errorCode;
        }

        //Get all ErrorCodes
        public async Task<ErrorCode[]> GetAllErrorCodesAsync()
        {
            IQueryable<ErrorCode> query = _appDbContext.ErrorCodes;
            return await query.ToArrayAsync();
        }

        //Create ErrorCode
        public async Task<int> AddErrorCodeAsync(ErrorCodeViewModal errorCode)
        {
            try
            {
                var errorCodeAdd = new ErrorCode
                {
                    errorCodeName = errorCode.errorCodeName,
                    errorCodeDescription = errorCode.errorCodeDescription
                };

                _appDbContext.ErrorCodes.Add(errorCodeAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        //Update ErrorCode
        public async Task<int> UpdateErrorCodeAsync(int errorCodeId, ErrorCodeViewModal errorCode)
        {
            try
            {
                var existingErrorCode = await _appDbContext.ErrorCodes.FindAsync(errorCodeId);

                if (existingErrorCode == null)
                {
                    return 404;
                }

                existingErrorCode.errorCodeName = errorCode.errorCodeName;
                existingErrorCode.errorCodeDescription = errorCode.errorCodeDescription;

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        //Delete ErrorCode
        public async Task<int> DeleteErrorCodeAsync(int errorCodeId)
        {
            var errorCode = await _appDbContext.ErrorCodes.FindAsync(errorCodeId);

            if (errorCode == null)
            {
                return 404;
            }

            _appDbContext.ErrorCodes.Remove(errorCode);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        //Search ErrorCode   
        public Task<List<ErrorCode>> SearchErrorCodeAsync(string searchString)
        {
            return _appDbContext.ErrorCodes
                .Where(x => x.errorCodeName.Contains(searchString))
                .ToListAsync();
        }
    }
}
