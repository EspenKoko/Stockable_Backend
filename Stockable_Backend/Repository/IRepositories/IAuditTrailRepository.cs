using Microsoft.Data.SqlClient;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IAuditTrailRepository
    {
        //AuditTrail
        Task<AuditTrail> GetAuditTrailAsync(int AuditTrailId);
        Task<AuditTrail[]> GetAllAuditTrailsAsync();
        Task<int> AddAuditTrailAsync(AuditTrailViewModal AuditTrail);
        Task<int> UpdateAuditTrailAsync(int AuditTrailId, AuditTrailViewModal AuditTrail);
        Task<int> DeleteAuditTrailAsync(int AuditTrailId);
        //IEnumerable<AuditTrail> GetAuditTrailsByDateAndUser(SqlParameter startDate, SqlParameter endDate, SqlParameter userId);
        //Task<List<AuditTrails>> SearchAuditTrailsAsync(string searchString);
    }
}
