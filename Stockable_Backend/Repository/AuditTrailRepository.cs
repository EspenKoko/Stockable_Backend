using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuditTrailRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //AuditTrail
        //Get all AuditTrail
        public async Task<AuditTrail[]> GetAllAuditTrailsAsync()
        {
            IQueryable<AuditTrail> query = _appDbContext.AuditTrails;
            return await query.ToArrayAsync();
        }

        //Get AuditTrail
        public async Task<AuditTrail> GetAuditTrailAsync(int AuditTrailId)
        {
            AuditTrail AuditTrail = await _appDbContext.AuditTrails
                .FirstOrDefaultAsync(x => x.auditTrailId == AuditTrailId);

            return AuditTrail;
        }

        // Create AuditTrail
        public async Task<int> AddAuditTrailAsync(AuditTrailViewModal AuditTrail)
        {
            try
            {
                AuditTrail AuditTrailAdd = new AuditTrail
                {
                    userAction = AuditTrail.userAction,
                    userName = AuditTrail.userName,
                    date = AuditTrail.date,
                    userId = AuditTrail.userId,
                };

                await _appDbContext.AuditTrails.AddAsync(AuditTrailAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;

            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update AuditTrail
        public async Task<int> UpdateAuditTrailAsync(int AuditTrailId, AuditTrailViewModal AuditTrail)
        {
            try
            {
                AuditTrail existingAuditTrail = await _appDbContext.AuditTrails.FindAsync(AuditTrailId);
                if (existingAuditTrail == null)
                {
                    return 404;
                }

                existingAuditTrail.userAction = AuditTrail.userAction;
                existingAuditTrail.userName = AuditTrail.userName;
                existingAuditTrail.date = AuditTrail.date;
                existingAuditTrail.userId = AuditTrail.userId;

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete AuditTrail
        public async Task<int> DeleteAuditTrailAsync(int AuditTrailId)
        {
            AuditTrail attemptToFindInDb = await _appDbContext.AuditTrails.FindAsync(AuditTrailId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.AuditTrails.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        //public IEnumerable<AuditTrail> GetAuditTrailsByDateAndUser(SqlParameter startDate, SqlParameter endDate, SqlParameter userId)
        //{
        //    // Implement the logic to execute the stored procedure
        //    var query = "EXEC GetAuditTrails @StartDate, @EndDate, @UserId";
        //    return _appDbContext.AuditTrails.FromSqlRaw(query, startDate, endDate, userId).ToList();
        //}
    }
}
