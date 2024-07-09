using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class SupplierOrderRepository : ISupplierOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public SupplierOrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Get all supplier orders
        public async Task<SupplierOrder[]> GetAllSupplierOrdersAsync()
        {
            IQueryable<SupplierOrder> query = _appDbContext.SupplierOrders
                .Include(c => c.supplierOrderStatus)
                .Include(c => c.supplier)
                .Include(c => c.employee.employeeType)
                .Include(c => c.employee.user);

            return await query.ToArrayAsync();
        }

        // Get supplier order
        public async Task<SupplierOrder> GetSupplierOrderAsync(int supplierOrderId)
        {
            return await _appDbContext.SupplierOrders
                .Include(c => c.supplierOrderStatus)
                .Include(c => c.supplier)
                .Include(c => c.employee).ThenInclude(employee => employee.user).FirstOrDefaultAsync(so => so.supplierOrderId == supplierOrderId);
        }

        // Create supplier order
        public async Task<int> AddSupplierOrderAsync(SupplierOrderViewModal supplierOrder)
        {
            try
            {
                Supplier supplier = await _appDbContext.Suppliers.FindAsync(supplierOrder.supplierId);
                SupplierOrderStatus supplierOrderStatus = await _appDbContext.SupplierOrderStatuses.FindAsync(supplierOrder.supplierOrderStatusId);
                Employee employee = await _appDbContext.Employees.FindAsync(supplierOrder.employeeId);

                if (supplier != null && supplierOrderStatus != null && employee != null)
                {
                    SupplierOrder supplierOrederAdd = new SupplierOrder
                    {
                        supplierId = supplierOrder.supplierId,
                        supplierOrderStatusId = supplierOrder.supplierOrderStatusId,
                        employeeId = supplierOrder.employeeId,
                        date = supplierOrder.date,
                    };

                    await _appDbContext.SupplierOrders.AddAsync(supplierOrederAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                return 404; // Supplier or SupplierOrderStatus or Employee not found
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }
        }

        // Update supplier order
        public async Task<int> UpdateSupplierOrderAsync(int supplierOrderId, SupplierOrderViewModal supplierOrder)
        {
            try
            {
                SupplierOrder existingSupplierOrder = await _appDbContext.SupplierOrders.FirstOrDefaultAsync(so => so.supplierOrderId == supplierOrderId);

                if (existingSupplierOrder == null)
                {
                    return 404; // Supplier order not found
                }


                Supplier supplier = await _appDbContext.Suppliers.FindAsync(supplierOrder.supplierId);
                SupplierOrderStatus supplierOrderStatus = await _appDbContext.SupplierOrderStatuses.FindAsync(supplierOrder.supplierOrderStatusId);
                Employee employee = await _appDbContext.Employees.FindAsync(supplierOrder.employeeId);

                if (supplier != null && supplierOrderStatus != null && employee != null)
                {
                    existingSupplierOrder.supplierId = supplierOrder.supplierId;
                    existingSupplierOrder.supplierOrderStatusId = supplierOrder.supplierOrderStatusId;
                    existingSupplierOrder.employeeId = supplierOrder.employeeId;
                    existingSupplierOrder.date = supplierOrder.date;

                    _appDbContext.SupplierOrders.Update(existingSupplierOrder);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid Supplier or SupplierOrderStatus or Employee
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }
        }

        // Delete supplier order
        public async Task<int> DeleteSupplierOrderAsync(int supplierOrderId)
        {
            SupplierOrder supplierOrderToDelete = await _appDbContext.SupplierOrders.FirstOrDefaultAsync(so => so.supplierOrderId == supplierOrderId);

            if (supplierOrderToDelete == null)
            {
                return 404; // Supplier order not found
            }

            _appDbContext.SupplierOrders.Remove(supplierOrderToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search branch
        public async Task<List<SupplierOrder>> SearchSupplierOrderAsync(string searchString)
        {
            List<SupplierOrder> supplierOrders = await _appDbContext.SupplierOrders.Include(c => c.supplierOrderStatus)
                .Include(c => c.supplier)
                .Include(c => c.employee).ThenInclude(employee => employee.user)
                .Where(x => x.supplier.supplierName.Contains(searchString)).ToListAsync();

            return supplierOrders;
        }
    }

}
