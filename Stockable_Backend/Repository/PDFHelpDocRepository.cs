using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class PDFHelpDocRepository : IPDFHelpDocRepository
    {
        private readonly AppDbContext _appDbContext;

        public PDFHelpDocRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //PDFHelpDoc
        //Get all PDFHelpDoc
        public async Task<PDFHelpDoc[]> GetAllPDFHelpDocsAsync()
        {
            IQueryable<PDFHelpDoc> query = _appDbContext.PDFHelpDocs;
            return await query.ToArrayAsync();
        }

        //Get PDFHelpDoc
        public async Task<PDFHelpDoc> GetPDFHelpDocAsync(int PDFHelpDocId)
        {
            PDFHelpDoc PDFHelpDoc = await _appDbContext.PDFHelpDocs
                .FirstOrDefaultAsync(x => x.docId == PDFHelpDocId);

            return PDFHelpDoc;
        }

        // Create PDFHelpDoc
        public async Task<int> AddPDFHelpDocAsync(PDFHelpDoc pdfHelpDoc)
        {
            try
            {
                await _appDbContext.PDFHelpDocs.AddAsync(pdfHelpDoc);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update PDFHelpDoc
        public async Task<int> UpdatePDFHelpDocAsync(int PDFHelpDocId, PDFHelpDocViewModal PDFHelpDoc)
        {
            try
            {
                PDFHelpDoc existingPDFHelpDoc = await _appDbContext.PDFHelpDocs.FindAsync(PDFHelpDocId);
                if (existingPDFHelpDoc == null)
                {
                    return 404;
                }

                existingPDFHelpDoc.userType = PDFHelpDoc.userType;

                if (PDFHelpDoc.pdfFile != null && PDFHelpDoc.pdfFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await PDFHelpDoc.pdfFile.CopyToAsync(memoryStream);
                        existingPDFHelpDoc.pdfContent = memoryStream.ToArray();
                    }
                }

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        // Delete PDFHelpDoc
        public async Task<int> DeletePDFHelpDocAsync(int PDFHelpDocId)
        {
            PDFHelpDoc attemptToFindInDb = await _appDbContext.PDFHelpDocs.FindAsync(PDFHelpDocId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.PDFHelpDocs.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }
    }
}
