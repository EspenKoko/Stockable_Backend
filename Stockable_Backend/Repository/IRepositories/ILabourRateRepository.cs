using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface ILabourRateRepository
    {
        //LabourRate
        Task<LabourRate> GetLabourRateAsync(int labourRateId);
        Task<LabourRate[]> GetAllLabourRatesAsync();
        Task<int> AddLabourRateAsync(LabourRateViewModal labourRate);
        Task<int> UpdateLabourRateAsync(int labourRateId, LabourRateViewModal labourRate);
        Task<int> DeleteLabourRateAsync(int labourRateId);
    }
}
