using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IAssignedTechnicianRepository
    {
        //AssignedTechnician
        Task<AssignedTechnician[]> GetAllAssignedTechnicianAsync();
        Task<List<AssignedTechnician>> GetAssignedTechnicianAsync(int errorLogId);
        Task<int> AddAssignedTechnicianAsync(AssignedTechnicianViewModal assignedTechnician);
        Task<int> UpdateAssignedTechnicianAsync(int errorLogId, AssignedTechnicianViewModal assignedTechnician);
        Task<int> DeleteAssignedTechnicianAsync(int errorLogId);
        Task<List<AssignedTechnician>> SearchAssignedTechnicianAsync(string searchString);
    }
}
