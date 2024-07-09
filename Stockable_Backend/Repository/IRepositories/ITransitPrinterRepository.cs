using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface ITransitPrinterRepository
    {
        Task<TransitPrinter> GetTransitPrinterAsync(int TransitPrinterId);
        Task<TransitPrinter[]> GetAllTransitPrintersAsync();
        Task<int> AddTransitPrinterAsync(TransitPrinterViewModal TransitPrinter);
        Task<int> UpdateTransitPrinterAsync(int TransitPrinterId, TransitPrinterViewModal TransitPrinter);
        Task<int> DeleteTransitPrinterAsync(int TransitPrinterId);
        Task<List<TransitPrinter>> SearchTransitPrinterAsync(string searchString);
    }
}
