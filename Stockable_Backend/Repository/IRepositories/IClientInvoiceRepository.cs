using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IClientInvoiceRepository
    {
        //ClientInvoice
        Task<ClientInvoice[]> GetAllClientInvoiceAsync();
        Task<ClientInvoice> GetClientInvoiceAsync(int clientinvoiceId);
        Task<int> AddClientInvoiceAsync(ClientInvoiceViewModal clientinvoice);
        Task<int> UpdateClientInvoiceAsync(int clientinvoiceId, ClientInvoiceViewModal clientinvoice);
        Task<int> DeleteClientInvoiceAsync(int clientinvoiceId);
        Task<List<ClientInvoice>> SearchClientInvoiceAsync(string searchString);
    }
}
