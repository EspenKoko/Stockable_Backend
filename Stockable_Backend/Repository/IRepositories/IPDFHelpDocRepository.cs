using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories

{
    public interface IPDFHelpDocRepository
    {
        //PDFHelpDoc
        Task<PDFHelpDoc> GetPDFHelpDocAsync(int PDFHelpDocId);
        Task<PDFHelpDoc[]> GetAllPDFHelpDocsAsync();
        Task<int> AddPDFHelpDocAsync(PDFHelpDoc pdfHelpDoc);
        Task<int> UpdatePDFHelpDocAsync(int PDFHelpDocId, PDFHelpDocViewModal pdfHelpDoc);
        Task<int> DeletePDFHelpDocAsync(int PDFHelpDocId);
    }
}
