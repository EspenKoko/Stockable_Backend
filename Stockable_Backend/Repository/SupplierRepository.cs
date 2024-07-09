using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _appDbContext;

        public SupplierRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Supplier
        //Get Supplier
        public async Task<Supplier> GetSupplierAsync(int SupplierId)
        {
            Supplier supplier = await _appDbContext.Suppliers.Where(x => x.supplierId == SupplierId).FirstOrDefaultAsync();

            return supplier;
        }

        //Get all Suppliers
        public async Task<Supplier[]> GetAllSuppliersAsync()
        {
            IQueryable<Supplier> query = _appDbContext.Suppliers;
            return await query.ToArrayAsync();
        }

        // Create Supplier
        public async Task<int> AddSupplierAsync(SupplierViewModal supplier)
        {
            try
            {
                Supplier supplierAdd = new Supplier
                {
                    supplierName = supplier.supplierName,
                    supplierAddress = supplier.supplierAddress,
                    supplierContactNumber = supplier.supplierContactNumber,
                    supplierEmail = supplier.supplierEmail
                };

                await _appDbContext.Suppliers.AddAsync(supplierAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update Supplier
        public async Task<int> UpdateSupplierAsync(int SupplierId, SupplierViewModal supplier)
        {
            try
            {
                Supplier attemptToFindInDb = await _appDbContext.Suppliers.FindAsync(SupplierId);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                attemptToFindInDb.supplierName = supplier.supplierName;
                attemptToFindInDb.supplierAddress = supplier.supplierAddress;
                attemptToFindInDb.supplierContactNumber = supplier.supplierContactNumber;
                attemptToFindInDb.supplierEmail = supplier.supplierEmail;

                _appDbContext.Suppliers.Update(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete Supplier
        public async Task<int> DeleteSupplierAsync(int SupplierId)
        {
            Supplier attemptToFindInDb = await _appDbContext.Suppliers.FindAsync(SupplierId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.Suppliers.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search Supplier
        public async Task<List<Supplier>> SearchSupplierAsync(string searchString)
        {
            List<Supplier> suppliers = await _appDbContext.Suppliers
                .Where(x => x.supplierName.Contains(searchString))
                .ToListAsync();

            return suppliers;
        }

    }
}
