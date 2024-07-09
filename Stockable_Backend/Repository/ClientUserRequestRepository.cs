using Stockable_Backend.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ClientUserRequestRepository : IClientUserRequestRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClientUserRequestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ClientUserRequest
        //Get all ClientUserRequests
        public async Task<ClientUserRequest[]> GetAllClientUserRequestsAsync()
        {
            IQueryable<ClientUserRequest> query = _appDbContext.ClientUserRequests
                .Include(b => b.branch.city.province)
                .Include(b => b.branch.client);
            return await query.ToArrayAsync();
        }

        //Get ClientUserRequest
        public async Task<ClientUserRequest> GetClientUserRequestAsync(int clientUserRequestId)
        {
            ClientUserRequest clientUserRequest = await _appDbContext.ClientUserRequests
                .Include(b => b.branch.city.province)
                .Include(b => b.branch.client)
                .FirstOrDefaultAsync(x => x.clientUserRequestId == clientUserRequestId);

            return clientUserRequest;
        }

        // Create ClientUserRequest
        public async Task<int> AddClientUserRequestAsync(ClientUserRequestViewModal clientUserRequest)
        {
            try
            {
                Branch branch = await _appDbContext.Branches.FindAsync(clientUserRequest.branchId);
                //Client client = await _appDbContext.Clients.FindAsync(clientUserRequest.clientId);

                if (branch != null /*&& client != null*/)
                {
                    ClientUserRequest clientUserRequestAdd = new ClientUserRequest
                    {
                        name = clientUserRequest.name,
                        surname = clientUserRequest.surname,
                        number = clientUserRequest.number,
                        email = clientUserRequest.email,
                        clientUserPosition = clientUserRequest.clientUserPosition,
                        userCreated = clientUserRequest.userCreated,
                        role = clientUserRequest.role,
                        branchId = branch.branchId,
                        //clientId = branch.clientId,
                    };

                    await _appDbContext.ClientUserRequests.AddAsync(clientUserRequestAdd);
                    await _appDbContext.SaveChangesAsync();

                    return 200;
                }
                return 404; // Client or city not found
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update ClientUserRequest
        public async Task<int> UpdateClientUserRequestAsync(int clientUserRequestId, ClientUserRequestViewModal clientUserRequest)
        {
            try
            {
                ClientUserRequest existingClientUserRequest = await _appDbContext.ClientUserRequests.FindAsync(clientUserRequestId);
                if (existingClientUserRequest == null)
                {
                    return 404;
                }

                existingClientUserRequest.name = clientUserRequest.name;
                existingClientUserRequest.surname = clientUserRequest.surname;
                existingClientUserRequest.number = clientUserRequest.number;
                existingClientUserRequest.email = clientUserRequest.email;
                existingClientUserRequest.clientUserPosition = clientUserRequest.clientUserPosition;
                existingClientUserRequest.userCreated = clientUserRequest.userCreated;
                existingClientUserRequest.role = clientUserRequest.role;

                Branch branch = await _appDbContext.Branches.FindAsync(clientUserRequest.branchId);
                //Client client = await _appDbContext.Clients.FindAsync(clientUserRequest.clientId);

                if (branch != null /*&& client != null*/)
                {
                    existingClientUserRequest.branchId = clientUserRequest.branchId;
                    //existingClientUserRequest.clientId = clientUserRequest.clientId;

                    await _appDbContext.SaveChangesAsync();

                    return 200;
                }
                else
                {
                    return 501;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete ClientUserRequest
        public async Task<int> DeleteClientUserRequestAsync(int clientUserRequestId)
        {
            ClientUserRequest attemptToFindInDb = await _appDbContext.ClientUserRequests.FindAsync(clientUserRequestId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.ClientUserRequests.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        // Search ClientUserRequest
        public async Task<List<ClientUserRequest>> SearchClientUserRequestAsync(string searchString)
        {
            List<ClientUserRequest> clientUserRequests = await _appDbContext.ClientUserRequests
                .Include(b => b.branch.city.province)
                .Include(b => b.branch.client)
                .Where(x => x.name.Contains(searchString)).ToListAsync();

            return clientUserRequests;
        }
    }
}
