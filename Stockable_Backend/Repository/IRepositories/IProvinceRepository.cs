using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IProvinceRepository
    {
        //Province
        Task<Province[]> GetAllProvincesAsync();
        Task<Province> GetProvinceAsync(int provinceId);
        Task<int> AddProvinceAsync(ProvinceViewModal province);
        Task<int> UpdateProvinceAsync(int provinceId, ProvinceViewModal province);
        Task<int> DeleteProvinceAsync(int provinceId);
        Task<List<Province>> SearchProvinceAsync(string searchString);
    }
}
