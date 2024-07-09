using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ClientOrderRepository : IClientOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClientOrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ClientOrder
        //Get all clientOrderes
        public async Task<ClientOrder[]> GetAllClientOrderAsync()
        {
            IQueryable<ClientOrder> query = _appDbContext.ClientOrders
            .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.client)
            .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.user)
            .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
            .Include(c => c.clientOrderStatus)
            //.Include(c => c.stock.stockType.stockCategory)
            .Include(c => c.clientInvoice)
            .Include(c => c.paymentType);

            return await query.ToArrayAsync();
        }

        //Get clientOrder
        public async Task<ClientOrder> GetClientOrderAsync(int clientOrderId)
        {
            ClientOrder clientOrder = await _appDbContext.ClientOrders
                .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.user)
                .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
                .Include(c => c.clientOrderStatus)
                //.Include(c => c.stock.stockType.stockCategory)
                .Include(c => c.clientInvoice)
                .Include(c => c.paymentType)
                .FirstOrDefaultAsync(x => x.clientOrderId == clientOrderId);

            return clientOrder;
        }

        //Create clientOrder
        public async Task<int> AddClientOrderAsync(ClientOrderViewModal clientOrder)
        {
            try
            {
                ClientUser clientUser = await _appDbContext.ClientUsers.FindAsync(clientOrder.clientUserId);
                ClientOrderStatus clientOrderStatus = await _appDbContext.ClientOrderStatus.FindAsync(clientOrder.clientOrderStatusId);
                ClientInvoice stockInvoice = await _appDbContext.ClientInvoices.FindAsync(clientOrder.clientInvoiceId);
                PaymentType paymentType = await _appDbContext.PaymentTypes.FindAsync(clientOrder.paymentTypeId);
                //Stock stock = await _appDbContext.Stocks.FindAsync(clientOrder.stockId);

                if (clientUser != null && clientOrderStatus != null && stockInvoice != null && paymentType != null /*&& stock != null*/)
                {
                    ClientOrder clientOrderAdd = new ClientOrder
                    {
                        //qty = clientOrder.qty,
                        clientUserId = clientOrder.clientUserId,
                        clientOrderStatusId = clientOrder.clientOrderStatusId,
                        clientInvoiceId = clientOrder.clientInvoiceId,
                        paymentTypeId = clientOrder.paymentTypeId,
                        //stockId = clientOrder.stockId,
                    };

                    await _appDbContext.ClientOrders.AddAsync(clientOrderAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }

                return 404; // Client user or Order status or Stock Invoice or Payment Type not found
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update clientOrder
        public async Task<int> UpdateClientOrderAsync(int clientOrderId, ClientOrderViewModal clientOrder)
        {
            try
            {
                // Find the object in the db 
                ClientOrder attemptToFindInDb = await _appDbContext.ClientOrders.FirstOrDefaultAsync(x => x.clientOrderId == clientOrderId);

                if (attemptToFindInDb == null)
                {
                    return 404; // ClientOrder not found
                }

                ClientUser clientUser = await _appDbContext.ClientUsers.FindAsync(clientOrder.clientUserId);
                ClientOrderStatus clientOrderStatus = await _appDbContext.ClientOrderStatus.FindAsync(clientOrder.clientOrderStatusId);
                ClientInvoice stockInvoice = await _appDbContext.ClientInvoices.FindAsync(clientOrder.clientInvoiceId);
                PaymentType paymentType = await _appDbContext.PaymentTypes.FindAsync(clientOrder.paymentTypeId);
                //Stock stock = await _appDbContext.Stocks.FindAsync(clientOrder.stockId);

                if (clientUser != null && clientOrderStatus != null && stockInvoice != null && paymentType != null/* && stock != null*/)
                {
                    attemptToFindInDb.clientUserId = clientOrder.clientUserId;
                    attemptToFindInDb.clientOrderStatusId = clientOrder.clientOrderStatusId;
                    attemptToFindInDb.clientInvoiceId = clientOrder.clientInvoiceId;
                    attemptToFindInDb.paymentTypeId = clientOrder.paymentTypeId;
                    //attemptToFindInDb.stockId = clientOrder.stockId;

                    _appDbContext.ClientOrders.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid Client user or Order status or Stock Invoice or Payment Type
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete clientOrder
        public async Task<int> DeleteClientOrderAsync(int clientOrderId)
        {
            // Find the object in the db 
            ClientOrder clientOrderToDelete = await _appDbContext.ClientOrders.FirstOrDefaultAsync(x => x.clientOrderId == clientOrderId);

            if (clientOrderToDelete == null)
            {
                return 404; // ClientOrder not found
            }

            _appDbContext.ClientOrders.Remove(clientOrderToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search clientOrder
        public async Task<List<ClientOrder>> SearchClientOrderAsync(string searchString)
        {
            List<ClientOrder> clientOrders = await _appDbContext.ClientOrders
                .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.client)
                .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.user)
                .Include(c => c.clientUser).ThenInclude(clientUser => clientUser.branch.city.province)
                .Include(c => c.clientOrderStatus)
                //.Include(c => c.stock.stockType.stockCategory)
                .Include(c => c.clientInvoice)
                .Include(c => c.paymentType)
                .Where(x => x.clientInvoice.clientInvoiceNumber.Contains(searchString)).ToListAsync();

            return clientOrders;
        }
    }
}
