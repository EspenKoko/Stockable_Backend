using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IAssignedPrinterRepository
    {
        //Printer
        Task<AssignedPrinter> GetPrinterAsync(int printerId);
        Task<AssignedPrinter[]> GetAllPrinterAsync();
        Task<int> AddPrinterAsync(AssignedPrinterViewModal printer);
        Task<int> UpdatePrinterAsync(int printerId, AssignedPrinterViewModal printer);
        Task<int> DeletePrinterAsync(int printerId);
        Task<List<AssignedPrinter>> SearchPrinterAsync(string searchString);
    }
}
