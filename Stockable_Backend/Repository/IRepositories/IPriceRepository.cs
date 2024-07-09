using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IPriceRepository
    {
        //Price
        Task<Price> GetPriceAsync(int priceId);
        Task<Price[]> GetAllPriceAsync();
        Task<int> AddPriceAsync(PriceViewModal price);
        Task<int> UpdatePriceAsync(int priceId, PriceViewModal price);
        Task<int> DeletePriceAsync(int priceId);
        Task<List<Price>> SearchPriceAsync(string searchString);

    }
}
