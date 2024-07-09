using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IPaymentTypeRepository
    {
        //PaymentType
        Task<PaymentType[]> GetAllPaymentTypeAsync();
        Task<PaymentType> GetPaymentTypeAsync(int paymentTypeId);
        Task<int> AddPaymentTypeAsync(PaymentTypeViewModal paymentType);
        Task<int> UpdatePaymentTypeAsync(int paymentTypeId, PaymentTypeViewModal paymentType);
        Task<int> DeletePaymentTypeAsync(int paymentTypeId);
        Task<List<PaymentType>> SearchPaymentTypeAsync(string searchString);
    }
}
