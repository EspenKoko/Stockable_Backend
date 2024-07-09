using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ClientRepository: IClientRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClientRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Client
        //Get all Clients
        public async Task<Client[]> GetAllClientsAsync()
        {
            IQueryable<Client> query = _appDbContext.Clients;
            return await query.ToArrayAsync();
        }

        //Get a Client
        public async Task<Client> GetClientAsync(int clientId)
        {
            Client client = await _appDbContext.Clients
                .FirstOrDefaultAsync(x => x.clientId == clientId);

            return client;
        }


        //Create Client
        public async Task<int> AddClientAsync(ClientViewModal client)
        {
            try
            {
                Client addClient = new Client
                {
                    clientName = client.clientName,
                    clientNumber = client.clientNumber,
                    clientEmail = client.clientEmail,
                    clientAddress = client.clientAddress
                };

                await _appDbContext.Clients.AddAsync(addClient);
                await _appDbContext.SaveChangesAsync();
                return 200;
            }
            catch
            {
                return 500;
            }
        }

        //Update Clinet
        public async Task<int> UpdateClientAsync(int clientID, ClientViewModal client)
        {
            try
            {
                Client attemptToFindInDb = await _appDbContext.Clients.SingleOrDefaultAsync(x => x.clientId == clientID);

                if (attemptToFindInDb == null)
                {
                    return 404; // Branch not found
                }

                attemptToFindInDb.clientName = client.clientName;
                attemptToFindInDb.clientNumber = client.clientNumber;
                attemptToFindInDb.clientEmail = client.clientEmail;
                attemptToFindInDb.clientAddress = client.clientAddress;

                await _appDbContext.SaveChangesAsync();
                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }

        }

        //Delete Client
        public async Task<int> DeleteClientAsync(int clientID)
        {
            
            Client clientToDelete = await _appDbContext.Clients.Where(x => x.clientId == clientID).FirstOrDefaultAsync();
            if (clientToDelete == null)
            {
                return 404; // Branch not found
            }

            _appDbContext.Clients.Remove(clientToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
         }

        //Search Client
        public async Task<List<Client>> SearchClientAsync(string searchString)
        {
            List<Client> clients = await _appDbContext.Clients.Where(x => x.clientName.Contains(searchString)).ToListAsync();

            return clients;
        }
    }
}
