using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IVatRepository
    {
        //Vat
        Task<VAT> GetVatAsync(int vatId);
        Task<VAT[]> GetAllVatsAsync();
        Task<int> AddVatAsync(VatViewModal vat);
        Task<int> UpdateVatAsync(int vatId, VatViewModal vat);
        Task<int> DeleteVatAsync(int vatId);
    }
}
