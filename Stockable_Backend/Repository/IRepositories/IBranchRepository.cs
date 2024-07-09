using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IBranchRepository
    {
        //Branch
        Task<Branch[]> GetAllBranchAsync();
        Task<Branch> GetBranchAsync(int branchId);
        Task<int> AddBranchAsync(BranchViewModal branch);
        Task<int> UpdateBranchAsync(int branchId, BranchViewModal branch);
        Task<int> DeleteBranchAsync(int branchId);
        Task<List<Branch>> SearchBranchAsync(string searchString);
    }
}
