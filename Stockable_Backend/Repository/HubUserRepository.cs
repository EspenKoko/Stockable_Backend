using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class HubUserRepository : IHubUserRepository
    {

        private readonly AppDbContext _appDbContext;

        public HubUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //HubUser
        //Get all HubUser
        public async Task<HubUser[]> GetAllHubUserAsync()
        {
            IQueryable<HubUser> query = _appDbContext.HubUsers.Include(c => c.user).Include(c => c.hub).ThenInclude(Hub => Hub.city).ThenInclude(Hub => Hub.province);
            return await query.ToArrayAsync();
        }

        //Get HubUser
        public async Task<HubUser> GetHubUserAsync(int hubUserId)
        {
            HubUser hubUser = await _appDbContext.HubUsers
                .Include(b => b.user)
                .Include(b => b.hub)
                .ThenInclude(Hub => Hub.city).ThenInclude(Hub => Hub.province)
                .FirstOrDefaultAsync(x => x.hubUserId == hubUserId);

            return hubUser;
        }


        // Create HubUser
        public async Task<int> AddHubUserAsync(HubUserViewModal hubUser)
        {
            try
            {
                HubUser hubUserAdd = new HubUser()
                {
                    hubUserName = hubUser.hubUserName,
                    hubUserSurname = hubUser.hubUserSurname,
                    hubUserPhoneNumber = hubUser.hubUserPhoneNumber,
                    hubUserEmail = hubUser.hubUserEmail,
                    hubUserPostion = hubUser.hubUserPostion
                };

                Hub hub = await _appDbContext.Hubs.FindAsync(hubUser.hubId);
                User user = await _appDbContext.Users.FindAsync(hubUser.userId);

                if (hub != null && user != null)
                {
                    hubUserAdd.hubId = hubUser.hubId;
                    hubUserAdd.userId = hubUser.userId;
                    await _appDbContext.HubUsers.AddAsync(hubUserAdd);
                    await _appDbContext.SaveChangesAsync();
                    return 200;
                }
                else
                {
                    return 404;
                }
            }
            catch (Exception)
            {
                return 500;
            }
        }


        // Update HubUser
        public async Task<int> UpdateHubUserAsync(int hubUserId, HubUserViewModal hubUser)
        {
            try
            {
                HubUser attemptToFindInDb = await _appDbContext.HubUsers.FindAsync(hubUserId);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                attemptToFindInDb.hubUserName = hubUser.hubUserName;
                attemptToFindInDb.hubUserSurname = hubUser.hubUserSurname;
                attemptToFindInDb.hubUserPhoneNumber = hubUser.hubUserPhoneNumber;
                attemptToFindInDb.hubUserEmail = hubUser.hubUserEmail;
                attemptToFindInDb.hubUserPostion = hubUser.hubUserPostion;

                Hub hub = await _appDbContext.Hubs.FindAsync(hubUser.hubId);
                User user = await _appDbContext.Users.FindAsync(hubUser.userId);

                if (hub != null && user != null)
                {
                    attemptToFindInDb.hubId = hubUser.hubId;
                    attemptToFindInDb.userId = hubUser.userId;
                    _appDbContext.HubUsers.Update(attemptToFindInDb);
                    await _appDbContext.SaveChangesAsync();
                    return 200;
                }
                else
                {
                    return 501;
                }
            }
            catch (Exception)
            {
                return 500;
            }
        }


        //Delete HubUser
        public async Task<int> DeleteHubUserAsync(int hubUserId)
        {
            //Find the object in the db 
            HubUser attemptToFindInDb = await _appDbContext.HubUsers.Where(x => x.hubUserId == hubUserId).FirstOrDefaultAsync();

            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.HubUsers.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();
        
            return 200;
        }
        //Search HubUser   
        public async Task<List<HubUser>> SearchHubUserAsync(string searchString)
        {
            List<HubUser> hubUsers = await _appDbContext.HubUsers.Include(c => c.user).Include(c => c.hub).ThenInclude(Hub => Hub.city).ThenInclude(Hub => Hub.province).Where(x => x.hubUserName.Contains(searchString)).ToListAsync();

            return hubUsers;
        }
    }
}
