using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ClientOrderStatusRepository : IClientOrderStatusRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClientOrderStatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ClientOrderStatus
        //Get all clientOrderStatuss
        public async Task<ClientOrderStatus[]> GetAllClientOrderStatussAsync()
        {
            IQueryable<ClientOrderStatus> query = _appDbContext.ClientOrderStatus;
            return await query.ToArrayAsync();
        }

        //Get clientOrderStatus
        public async Task<ClientOrderStatus> GetClientOrderStatusAsync(int clientOrderStatusId)
        {
            ClientOrderStatus clientOrderStatus = await _appDbContext.ClientOrderStatus.FirstOrDefaultAsync(x => x.clientOrderStatusId == clientOrderStatusId);

            return clientOrderStatus;
        }


        //Create clientOrderStatus
        public async Task<int> AddClientOrderStatusAsync(ClientOrderStatusViewModal clientOrderStatus)
        {
            try
            {
                ClientOrderStatus clientOrderStatusAdd = new ClientOrderStatus
                {
                    clientOrderStatusName = clientOrderStatus.clientOrderStatusName
                };

                _appDbContext.ClientOrderStatus.Add(clientOrderStatusAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal Server Error
            }
        }

        //Update clientOrderStatus
        public async Task<int> UpdateClientOrderStatusAsync(int clientOrderStatusId, ClientOrderStatusViewModal clientOrderStatus)
        {
            // Find the object in the db 
            ClientOrderStatus attemptToFindInDb = await _appDbContext.ClientOrderStatus.FirstOrDefaultAsync(x => x.clientOrderStatusId == clientOrderStatusId);

            if (attemptToFindInDb == null)
            {
                return 404; // ClientOrderStatus not found
            }

            attemptToFindInDb.clientOrderStatusName = clientOrderStatus.clientOrderStatusName;

            _appDbContext.ClientOrderStatus.Update(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        //Delete ClientOrderStatus
        public async Task<int> DeleteClientOrderStatusAsync(int clientOrderStatusId)
        {
            // Find the object in the db 
            ClientOrderStatus attemptToFindInDb = await _appDbContext.ClientOrderStatus.FirstOrDefaultAsync(x => x.clientOrderStatusId == clientOrderStatusId);

            if (attemptToFindInDb == null)
            {
                return 404; // ClientOrderStatus not found
            }

            _appDbContext.ClientOrderStatus.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search ClientOrderStatus   
        public async Task<List<ClientOrderStatus>> SearchClientOrderStatusAsync(string searchString)
        {
            List<ClientOrderStatus> clientOrderStatuss = await _appDbContext.ClientOrderStatus.Where(x => x.clientOrderStatusName.Contains(searchString)).ToListAsync();

            return clientOrderStatuss;
        }
    }
}
