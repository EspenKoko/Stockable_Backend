using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IHelpRepository
    {
        //Help
        Task<Help[]> GetAllHelpAsync();
        Task<Help> GetHelpAsync(int HelpId);
        Task<int> AddHelpAsync(HelpViewModal Help);
        Task<int> UpdateHelpAsync(int HelpId, HelpViewModal Help);
        Task<int> DeleteHelpAsync(int HelpId);
        Task<List<Help>> SearchHelpAsync(string searchString);
    }
}
