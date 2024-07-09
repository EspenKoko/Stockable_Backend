using Stockable_Backend.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class RepairOrdersRepository : IRepairOrdersRepository
    {
        private readonly AppDbContext _appDbContext;

        public RepairOrdersRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //RepairOrder
        //Get all RepairOrders
        public async Task<RepairOrders[]> GetAllRepairOrdersAsync()
        {
            IQueryable<RepairOrders> query = _appDbContext.RepairOrders;
            return await query.ToArrayAsync();
        }

        //Get RepairOrder
        public async Task<RepairOrders> GetRepairOrderAsync(int RepairOrdersId)
        {
            RepairOrders RepairOrder = await _appDbContext.RepairOrders
                .FirstOrDefaultAsync(x => x.repairOrderId == RepairOrdersId);

            return RepairOrder;
        }

        // Create RepairOrder
        public async Task<int> AddRepairOrderAsync(RepairOrdersViewModal RepairOrder)
        {
            try
            {
                RepairOrders RepairOrderAdd = new RepairOrders
                {
                    vat = RepairOrder.vat,
                    markUp = RepairOrder.markUp,
                    labourRate = RepairOrder.labourRate,
                    date = RepairOrder.date,
                    serialNumber = RepairOrder.serialNumber,
                    total = RepairOrder.total,
                    client = RepairOrder.client,
                    branchCode = RepairOrder.branchCode,
                    repairId = RepairOrder.repairId,
                };

                await _appDbContext.RepairOrders.AddAsync(RepairOrderAdd);
                await _appDbContext.SaveChangesAsync();

            return 200;
                
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update RepairOrder
        public async Task<int> UpdateRepairOrderAsync(int RepairOrdersId, RepairOrdersViewModal RepairOrder)
        {
            try
            {
                RepairOrders existingRepairOrder = await _appDbContext.RepairOrders.FindAsync(RepairOrdersId);
                if (existingRepairOrder == null)
                {
                    return 404;
                }

                existingRepairOrder.vat = RepairOrder.vat;
                existingRepairOrder.markUp = RepairOrder.markUp;
                existingRepairOrder.labourRate = RepairOrder.labourRate;
                existingRepairOrder.date = RepairOrder.date;
                existingRepairOrder.serialNumber = RepairOrder.serialNumber;
                existingRepairOrder.total = RepairOrder.total;
                existingRepairOrder.client = RepairOrder.client;
                existingRepairOrder.branchCode = RepairOrder.branchCode;
                existingRepairOrder.repairId = RepairOrder.repairId;

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete RepairOrder
        public async Task<int> DeleteRepairOrderAsync(int RepairOrdersId)
        {
            RepairOrders attemptToFindInDb = await _appDbContext.RepairOrders.FindAsync(RepairOrdersId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.RepairOrders.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search RepairOrder
        public async Task<List<RepairOrders>> SearchRepairOrderAsync(string searchString)
        {
            List<RepairOrders> RepairOrders = await _appDbContext.RepairOrders
                .Where(x => x.serialNumber.Contains(searchString)).ToListAsync();

            return RepairOrders;
        }
    }
}
