using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class PurchaseOrderStatusRepository : IPurchaseOrderStatusRepository
    {
        private readonly AppDbContext _appDbContext;

        public PurchaseOrderStatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //PurchaseOrderStatus
        //Get all purchaseOrderStatuss
        public async Task<PurchaseOrderStatus[]> GetAllPurchaseOrderStatusAsync()
        {
            IQueryable<PurchaseOrderStatus> query = _appDbContext.PurchaseOrderStatuses;
            return await query.ToArrayAsync();
        }

        //Get purchaseOrderStatus
        public async Task<PurchaseOrderStatus> GetPurchaseOrderStatusAsync(int purchaseOrderStatusId)
        {
            PurchaseOrderStatus purchaseOrderStatus = await _appDbContext.PurchaseOrderStatuses.FirstOrDefaultAsync(x => x.purchaseOrderStatusId == purchaseOrderStatusId);

            return purchaseOrderStatus;
        }


        //Create purchaseOrderStatus
        public async Task<int> AddPurchaseOrderStatusAsync(PurchaseOrderStatusViewModal purchaseOrderStatus)
        {
            try
            {
                PurchaseOrderStatus purchaseOrderStatusAdd = new PurchaseOrderStatus
                {
                    purchaseOrderStatusName = purchaseOrderStatus.purchaseOrderStatusName
                };

                _appDbContext.PurchaseOrderStatuses.Add(purchaseOrderStatusAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal Server Error
            }
        }

        //Update purchaseOrderStatus
        public async Task<int> UpdatePurchaseOrderStatusAsync(int purchaseOrderStatusId, PurchaseOrderStatusViewModal purchaseOrderStatus)
        {
            // Find the object in the db 
            PurchaseOrderStatus attemptToFindInDb = await _appDbContext.PurchaseOrderStatuses.FirstOrDefaultAsync(x => x.purchaseOrderStatusId == purchaseOrderStatusId);

            if (attemptToFindInDb == null)
            {
                return 404; // PurchaseOrderStatus not found
            }

            attemptToFindInDb.purchaseOrderStatusName = purchaseOrderStatus.purchaseOrderStatusName;

            _appDbContext.PurchaseOrderStatuses.Update(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        //Delete PurchaseOrderStatus
        public async Task<int> DeletePurchaseOrderStatusAsync(int purchaseOrderStatusId)
        {
            // Find the object in the db 
            PurchaseOrderStatus attemptToFindInDb = await _appDbContext.PurchaseOrderStatuses.FirstOrDefaultAsync(x => x.purchaseOrderStatusId == purchaseOrderStatusId);

            if (attemptToFindInDb == null)
            {
                return 404; // PurchaseOrderStatus not found
            }

            _appDbContext.PurchaseOrderStatuses.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search PurchaseOrderStatus   
        public async Task<List<PurchaseOrderStatus>> SearchPurchaseOrderStatusAsync(string searchString)
        {
            List<PurchaseOrderStatus> purchaseOrderStatuss = await _appDbContext.PurchaseOrderStatuses.Where(x => x.purchaseOrderStatusName.Contains(searchString)).ToListAsync();

            return purchaseOrderStatuss;
        }
    }
}
