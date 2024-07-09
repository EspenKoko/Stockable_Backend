using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ClientOrderStockRepository : IClientOrderStockRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClientOrderStockRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ClientOrderStock
        //Get all ClientOrderStock
        public async Task<ClientOrderStock[]> GetAllCLientOrderStockAsync()
        {
            IQueryable<ClientOrderStock> query = _appDbContext.ClientOrderStocks
            .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
            .Include(c => c.clientOrder).ThenInclude(stock => stock.clientOrderStatus)
            .Include(c => c.clientOrder).ThenInclude(stock => stock.paymentType)
            .Include(c => c.clientOrder).ThenInclude(stock => stock.clientInvoice)
            .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.user)
            .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.client)
            .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.branch.city.province);

            return await query.ToArrayAsync();
        }

        // Get ClientOrderStocks
        public async Task<List<ClientOrderStock>> GetCLientOrderStockAsync(int clientOrderId)
        {
            List<ClientOrderStock> matchingCLientOrderStocks = await _appDbContext.ClientOrderStocks
                .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientOrderStatus)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.paymentType)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientInvoice)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.user)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.client)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.branch.city.province)
                .Where(x => x.clientOrderId == clientOrderId)
                .ToListAsync();

            return matchingCLientOrderStocks;
        }

        //Create clientOrderStock
        public async Task<int> AddCLientOrderStockAsync(ClientOrderStockViewModal clientOrderStock)
        {
            try
            {
                Stock stock = await _appDbContext.Stocks.FindAsync(clientOrderStock.stockId);
                ClientOrder clientOrder = await _appDbContext.ClientOrders.FindAsync(clientOrderStock.clientOrderId);


                if (stock != null && clientOrder != null)
                {
                    ClientOrderStock clientOrderStockAdd = new ClientOrderStock
                    {
                        stockId = clientOrderStock.stockId,
                        clientOrderId = clientOrderStock.clientOrderId,
                        qty = clientOrderStock.qty
                    };

                    await _appDbContext.ClientOrderStocks.AddAsync(clientOrderStockAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Stock or client order not found
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update clientOrderStock
        public async Task<int> UpdateCLientOrderStockAsync(int clientOrderId, ClientOrderStockViewModal clientOrderStock)
        {
            try
            {
                // Find the object in the db 
                ClientOrderStock attemptToFindInDb = await _appDbContext.ClientOrderStocks.FirstOrDefaultAsync(x => x.clientOrderId == clientOrderId);

                if (attemptToFindInDb == null)
                {
                    return 404; // ClientOrderStock not found
                }

                Stock stock = await _appDbContext.Stocks.FindAsync(clientOrderStock.stockId);
                ClientOrder clientOrder = await _appDbContext.ClientOrders.FindAsync(clientOrderStock.clientOrderId);


                if (stock != null && clientOrder != null)
                {
                    attemptToFindInDb.stockId = clientOrderStock.stockId;
                    attemptToFindInDb.clientOrderId = clientOrderStock.clientOrderId;
                    attemptToFindInDb.qty = clientOrderStock.qty;

                    _appDbContext.ClientOrderStocks.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid Stock or client order
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete clientOrderStock
        public async Task<int> DeleteCLientOrderStockAsync(int clientOrderId)
        {
            // Find the object in the db 
            List<ClientOrderStock> clientOrderStockToDelete = await _appDbContext.ClientOrderStocks
            .Where(x => x.clientOrderId == clientOrderId)
            .ToListAsync();

            if (clientOrderStockToDelete.Count == 0)
            {
                return 404; // StockSupplierOrder not found
            }

            _appDbContext.ClientOrderStocks.RemoveRange(clientOrderStockToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        // Delete stockSupplierOrder based on supplierOrderId and stockItemId
        public async Task<int> DeleteCLientOrderStockItemAsync(int clientOrderId, int stockId)
        {
            // Find the object in the db with the given supplierOrderId and stockItemId
            ClientOrderStock clientOrderStockToDelete = await _appDbContext.ClientOrderStocks
                .FirstOrDefaultAsync(x => x.clientOrderId == clientOrderId && x.stockId == stockId);

            if (clientOrderStockToDelete == null)
            {
                return 404; // StockSupplierOrder not found
            }

            _appDbContext.ClientOrderStocks.Remove(clientOrderStockToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search stockSupplierOrder
        public async Task<List<ClientOrderStock>> SearchCLientOrderStockAsync(string searchString)
        {
            List<ClientOrderStock> cLientOrderStocks = await _appDbContext.ClientOrderStocks
                .Include(c => c.stock).ThenInclude(stock => stock.stockType.stockCategory)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientOrderStatus)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.paymentType)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientInvoice)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.user)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.client)
                .Include(c => c.clientOrder).ThenInclude(stock => stock.clientUser.branch.city.province)
                .Where(x => x.clientOrder.clientUser.client.clientName.Contains(searchString)).ToListAsync();

            return cLientOrderStocks;
        }

    }
}
