using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IMarkupRepository
    {
        //Markup
        Task<Markup> GetMarkupAsync(int markupId);
        Task<Markup[]> GetAllMarkupsAsync();
        Task<int> AddMarkupAsync(MarkupViewModal markup);
        Task<int> UpdateMarkupAsync(int markupId, MarkupViewModal markup);
        Task<int> DeleteMarkupAsync(int markupId);
    }
}
