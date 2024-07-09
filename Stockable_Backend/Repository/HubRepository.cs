using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class HubRepository: IHubRepository
    {

        private readonly AppDbContext _appDbContext;

        public HubRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Hub
        //Get hub
        public async Task<Hub> GetHubAsync(int hubId)
        {
            Hub hub = await _appDbContext.Hubs
                .Include(h => h.city).ThenInclude(c => c.province)
                .FirstOrDefaultAsync(x => x.hubId == hubId);

            return hub;
        }


        //Get all hubs
        public async Task<Hub[]> GetAllHubsAsync()
        {
            IQueryable<Hub> query = _appDbContext.Hubs.Include(c => c.city).ThenInclude(c => c.province);
            return await query.ToArrayAsync();
        }

        //Create hub
        public async Task<int> AddHubAsync(HubViewModal hub)
        {
            try
            {
                City city = await _appDbContext.Cities.FindAsync(hub.cityId);
                if (city == null)
                {
                    return 501;
                }

                Hub hubAdd = new Hub
                {
                    hubName = hub.hubName,
                    qtyOnHand = hub.qtyOnHand,
                    cityId = hub.cityId
                };

                await _appDbContext.Hubs.AddAsync(hubAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        //update hub
        public async Task<int> UpdateHubAsync(int HubID, HubViewModal hub)
        {
            try
            {
                Hub attemptToFindInDb = await _appDbContext.Hubs.FindAsync(HubID);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                City city = await _appDbContext.Cities.FindAsync(hub.cityId);
                if (city == null)
                {
                    return 501; // Invalid City
                }

                attemptToFindInDb.hubName = hub.hubName;
                attemptToFindInDb.qtyOnHand = hub.qtyOnHand;
                attemptToFindInDb.cityId = hub.cityId;

                _appDbContext.Hubs.Update(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        //Delete hub
        public async Task<int> DeleteHubAsync(int hubID)
        {
            try
            {
                Hub attemptToFindInDb = await _appDbContext.Hubs.FindAsync(hubID);
                if (attemptToFindInDb == null)
                {
                    return 404;
                }

                _appDbContext.Hubs.Remove(attemptToFindInDb);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        //Search Hub   
        public async Task<List<Hub>> SearchHubAsync(string searchString)
        {
            List<Hub> hubs = await _appDbContext.Hubs.Include(c => c.city).ThenInclude(city => city.province).Where(x => x.hubName.Contains(searchString)).ToListAsync();

            return hubs;
        }
    }
}
