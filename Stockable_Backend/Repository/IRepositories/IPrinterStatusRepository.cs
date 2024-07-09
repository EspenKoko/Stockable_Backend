using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IPrinterStatusRepository
    {
        //PrinterStatus
        Task<PrinterStatus> GetPrinterStatusAsync(int printerStatusId);
        Task<PrinterStatus[]> GetAllPrinterStatusAsync();
        Task<int> AddPrinterStatusAsync(PrinterStatusViewModal printerStatus);
        Task<int> UpdatePrinterStatusAsync(int printerStatusId, PrinterStatusViewModal printerStatus);
        Task<List<PrinterStatus>> SearchPrinterStatusAsync(string searchString);
        Task<int> DeletePrinterStatusAsync(int printerStatusId);
    }
}
