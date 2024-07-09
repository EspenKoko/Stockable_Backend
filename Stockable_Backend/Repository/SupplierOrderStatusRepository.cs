using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class SupplierOrderStatusRepository : ISupplierOrderStatusRepository
    {
        private readonly AppDbContext _appDbContext;

        public SupplierOrderStatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //SupplierOrderStatus
        //Get all supplierOrderStatuss
        public async Task<SupplierOrderStatus[]> GetAllSupplierOrderStatusAsync()
        {
            IQueryable<SupplierOrderStatus> query = _appDbContext.SupplierOrderStatuses;
            return await query.ToArrayAsync();
        }

        //Get supplierOrderStatus
        public async Task<SupplierOrderStatus> GetSupplierOrderStatusAsync(int supplierOrderStatusId)
        {
            SupplierOrderStatus supplierOrderStatus = await _appDbContext.SupplierOrderStatuses.FirstOrDefaultAsync(x => x.supplierOrderStatusId == supplierOrderStatusId);

            return supplierOrderStatus;
        }


        //Create supplierOrderStatus
        public async Task<int> AddSupplierOrderStatusAsync(SupplierOrderStatusViewModal supplierOrderStatus)
        {
            try
            {
                SupplierOrderStatus supplierOrderStatusAdd = new SupplierOrderStatus
                {
                    supplierOrderStatusName = supplierOrderStatus.supplierOrderStatusName
                };

                _appDbContext.SupplierOrderStatuses.Add(supplierOrderStatusAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal Server Error
            }
        }

        //Update supplierOrderStatus
        public async Task<int> UpdateSupplierOrderStatusAsync(int supplierOrderStatusId, SupplierOrderStatusViewModal supplierOrderStatus)
        {
            // Find the object in the db 
            SupplierOrderStatus attemptToFindInDb = await _appDbContext.SupplierOrderStatuses.FirstOrDefaultAsync(x => x.supplierOrderStatusId == supplierOrderStatusId);

            if (attemptToFindInDb == null)
            {
                return 404; // SupplierOrderStatus not found
            }

            attemptToFindInDb.supplierOrderStatusName = supplierOrderStatus.supplierOrderStatusName;

            _appDbContext.SupplierOrderStatuses.Update(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        //Delete SupplierOrderStatus
        public async Task<int> DeleteSupplierOrderStatusAsync(int supplierOrderStatusId)
        {
            // Find the object in the db 
            SupplierOrderStatus attemptToFindInDb = await _appDbContext.SupplierOrderStatuses.FirstOrDefaultAsync(x => x.supplierOrderStatusId == supplierOrderStatusId);

            if (attemptToFindInDb == null)
            {
                return 404; // SupplierOrderStatus not found
            }

            _appDbContext.SupplierOrderStatuses.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search SupplierOrderStatus   
        public async Task<List<SupplierOrderStatus>> SearchSupplierOrderStatusAsync(string searchString)
        {
            List<SupplierOrderStatus> supplierOrderStatuss = await _appDbContext.SupplierOrderStatuses.Where(x => x.supplierOrderStatusName.Contains(searchString)).ToListAsync();

            return supplierOrderStatuss;
        }
    }
}
