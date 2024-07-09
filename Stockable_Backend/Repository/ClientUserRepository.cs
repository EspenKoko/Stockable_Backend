using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ClientUserRepository : IClientUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClientUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ClientUser
        //Get all ClientUsers
        public async Task<ClientUser[]> GetAllClientUsersAsync()
        {
            IQueryable<ClientUser> query = _appDbContext.ClientUsers
                .Include(c => c.client)
                .Include(c => c.user)
                .Include(c => c.branch).ThenInclude(branch => branch.city).ThenInclude(city => city.province);
            return await query.ToArrayAsync();
        }

        //Get ClientUser
        public async Task<ClientUser> GetClientUserAsync(int clientUserId)
        {

            ClientUser clientUser = await _appDbContext.ClientUsers
                .Include(b => b.client)
                .Include(b => b.user)
                .FirstOrDefaultAsync(x => x.clientUserId == clientUserId);

            return clientUser;

        }


        //Create ClientUser
        public async Task<int> AddClientUserAsync(ClientUserViewModal clientUser)
        {
            try
            {
                Client client = await _appDbContext.Clients.FindAsync(clientUser.clientId);
                User user = await _appDbContext.Users.FindAsync(clientUser.userId);
                Branch branch = await _appDbContext.Branches.FindAsync(clientUser.branchId);

                if (client != null && user != null && branch != null)
                {
                    ClientUser clientUserAdd = new ClientUser
                    {
                        clientUserPosition = clientUser.clientUserPosition,
                        clientId = clientUser.clientId,
                        userId = clientUser.userId,
                        branchId= clientUser.branchId,
                    };

                    await _appDbContext.ClientUsers.AddAsync(clientUserAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 404; // Invalid client or user
                }
            }
            catch (Exception e )
            {
                return 500; // Internal server error
            }
        }

        //Update ClientUser
        public async Task<int> UpdateClientUserAsync(int clientUserId, ClientUserViewModal clientUser)
        {
            try
            {
                ClientUser attemptToFindInDb = await _appDbContext.ClientUsers.FirstOrDefaultAsync(x => x.clientUserId == clientUserId);
                if (attemptToFindInDb == null) 
                {
                    return 404;
                }                 

                attemptToFindInDb.clientUserPosition = clientUser.clientUserPosition;

                Client client = await _appDbContext.Clients.FindAsync(clientUser.clientId);
                User user = await _appDbContext.Users.FindAsync(clientUser.userId);
                Branch branch = await _appDbContext.Branches.FindAsync(clientUser.branchId);

                if (client != null && user != null && branch != null)
                {
                    attemptToFindInDb.clientId = clientUser.clientId;
                    attemptToFindInDb.userId = clientUser.userId;
                    attemptToFindInDb.branchId = clientUser.branchId;

                    _appDbContext.ClientUsers.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();

                    return 200; // Success
                }
                else
                {
                    return 501; // Invalid client or user
                }
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }
        }

        //Delete ClientUser
        public async Task<int> DeleteClientUserAsync(int clientUserID)
        {
            //Find the object in the db 
            ClientUser clientUserToDelete = await _appDbContext.ClientUsers.Where(x => x.clientUserId == clientUserID).FirstOrDefaultAsync();

            if (clientUserToDelete == null)
            {
                return 404;
            }

            _appDbContext.ClientUsers.Remove(clientUserToDelete);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        //Search ClientUser   
        public async Task<List<ClientUser>> SearchClientUserAsync(string searchString)
        {
            List<ClientUser> clientUsers = await _appDbContext.ClientUsers.Include(c => c.client).Include(c => c.user).Where(x => x.user.userFirstName.Contains(searchString)).ToListAsync();

            return clientUsers;
        }
    }
}
