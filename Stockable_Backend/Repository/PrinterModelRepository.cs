//using Microsoft.EntityFrameworkCore;
//using Stockable_Backend.Model;
//using Stockable_Backend.Repository.IRepositories;
//using Stockable_Backend.ViewModel;

//namespace Stockable_Backend.Repository
//{
//    public class PrinterModelRepository : IPrinterModelRepository
//    {
//        private readonly AppDbContext _appDbContext;

//        public PrinterModelRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }

//        //PrinterModel
//        //Get PrinterModal
//        public async Task<PrinterModel> GetPrinterModelAsync(int printerModelId)
//        {
//            PrinterModel printerModel = await _appDbContext.PrinterModels.Where(x => x.printerModelId == printerModelId).FirstOrDefaultAsync();

//            return printerModel;
//        }

//        //Get all PrinterModels
//        public async Task<PrinterModel[]> GetAllPrinterModelsAsync()
//        {
//            IQueryable<PrinterModel> query = _appDbContext.PrinterModels;
//            return await query.ToArrayAsync();
//        }

//        // Create PrinterModel
//        public async Task<int> AddPrinterModelAsync(PrinterModelViewModal printerModel)
//        {
//            try
//            {
//                PrinterModel printerModelAdd = new PrinterModel
//                {
//                    printerModelName = printerModel.printerModelName,
//                    printerModelDescription = printerModel.printerModelDescription,
//                    printerModelBrand = printerModel.printerModelBrand
//                };

//                await _appDbContext.PrinterModels.AddAsync(printerModelAdd);
//                await _appDbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                return 500;
//            }

//            return 200;
//        }

//        // Update PrinterModel
//        public async Task<int> UpdatePrinterModelAsync(int printerModelId, PrinterModelViewModal printerModel)
//        {
//            PrinterModel attemptToFindInDb = await _appDbContext.PrinterModels.FindAsync(printerModelId);

//            if (attemptToFindInDb == null)
//            {
//                return 404;
//            }

//            attemptToFindInDb.printerModelName = printerModel.printerModelName;
//            attemptToFindInDb.printerModelDescription = printerModel.printerModelDescription;
//            attemptToFindInDb.printerModelBrand = printerModel.printerModelBrand;

//            await _appDbContext.SaveChangesAsync();

//            return 200;
//        }

//        // Delete PrinterModel
//        public async Task<int> DeletePrinterModelAsync(int printerModelId)
//        {
//            PrinterModel attemptToFindInDb = await _appDbContext.PrinterModels.FindAsync(printerModelId);

//            if (attemptToFindInDb == null)
//            {
//                return 404;
//            }

//            _appDbContext.PrinterModels.Remove(attemptToFindInDb);
//            await _appDbContext.SaveChangesAsync();

//            return 200;
//        }

//        // Search PrinterModel
//        public async Task<List<PrinterModel>> SearchPrinterModelAsync(string searchString)
//        {
//            List<PrinterModel> printerModels = await _appDbContext.PrinterModels
//                .Where(x => x.printerModelName.Contains(searchString))
//                .ToListAsync();

//            return printerModels;
//        }
//    }
//}
