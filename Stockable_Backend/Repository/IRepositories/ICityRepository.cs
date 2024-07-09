using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface ICityRepository
    {
        //City
        Task<City> GetCityAsync(int cityId);
        Task<City[]> GetAllCitiesAsync();
        Task<int> AddCityAsync(CityViewModal city);
        Task<int> UpdateCityAsync(int cityId, CityViewModal city);
        Task<int> DeleteCityAsync(int cityId);
        Task<List<City>> SearchCityAsync(string searchString);

    }
}
