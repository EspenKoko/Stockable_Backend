using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ClientInvoiceRepository : IClientInvoiceRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClientInvoiceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ClientInvoice
        //Get all clientInvoices
        public async Task<ClientInvoice[]> GetAllClientInvoiceAsync()
        {
            IQueryable<ClientInvoice> query = _appDbContext.ClientInvoices;

            return await query.ToArrayAsync();
        }

        //Get clientInvoice
        public async Task<ClientInvoice> GetClientInvoiceAsync(int clientInvoiceId)
        {
            ClientInvoice clientInvoice = await _appDbContext.ClientInvoices
                .FirstOrDefaultAsync(x => x.clientInvoiceId == clientInvoiceId);

            return clientInvoice;
        }

        //Create clientInvoice
        public async Task<int> AddClientInvoiceAsync(ClientInvoiceViewModal clientInvoice)
        {
            try
            {
                ClientInvoice clientInvoiceAdd = new ClientInvoice
                {
                    clientInvoiceNumber = clientInvoice.clientInvoiceNumber,
                    clientInvoiceDate = clientInvoice.clientInvoiceDate,
                };

                await _appDbContext.ClientInvoices.AddAsync(clientInvoiceAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception ex)
            {
                return 500; // Internal server error
            }
        }

        //Update clientInvoice
        public async Task<int> UpdateClientInvoiceAsync(int clientInvoiceId, ClientInvoiceViewModal clientInvoice)
        {
            try
            {
                // Find the object in the db 
                ClientInvoice attemptToFindInDb = await _appDbContext.ClientInvoices.FirstOrDefaultAsync(x => x.clientInvoiceId == clientInvoiceId);

                if (attemptToFindInDb == null)
                {
                    return 404; // ClientInvoice not found
                }

                attemptToFindInDb.clientInvoiceNumber = clientInvoice.clientInvoiceNumber;
                attemptToFindInDb.clientInvoiceDate = clientInvoice.clientInvoiceDate;

                _appDbContext.ClientInvoices.Update(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }

        }

        //Delete clientInvoice
        public async Task<int> DeleteClientInvoiceAsync(int clientInvoiceId)
        {
            // Find the object in the db 
            ClientInvoice clientInvoiceToDelete = await _appDbContext.ClientInvoices.FirstOrDefaultAsync(x => x.clientInvoiceId == clientInvoiceId);

            if (clientInvoiceToDelete == null)
            {
                return 404; // ClientInvoice not found
            }

            _appDbContext.ClientInvoices.Remove(clientInvoiceToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search clientInvoice
        public async Task<List<ClientInvoice>> SearchClientInvoiceAsync(string searchString)
        {
            List<ClientInvoice> clientInvoicees = await _appDbContext.ClientInvoices.Where(x => x.clientInvoiceNumber.Contains(searchString)).ToListAsync();

            return clientInvoicees;
        }
    }
}
