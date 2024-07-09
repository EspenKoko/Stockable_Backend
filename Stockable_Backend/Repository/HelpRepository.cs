using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class HelpRepository : IHelpRepository
    {
        private readonly AppDbContext _appDbContext;

        public HelpRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Help
        //Get all Helpes
        public async Task<Help[]> GetAllHelpAsync()
        {
            IQueryable<Help> query = _appDbContext.Help;

            return await query.ToArrayAsync();
        }


        //Get Help
        public async Task<Help> GetHelpAsync(int helpId)
        {
            Help help = await _appDbContext.Help.FirstOrDefaultAsync(x => x.helpId == helpId);

            return help;
        }

        //Create help
        public async Task<int> AddHelpAsync(HelpViewModal help)
        {
            try
            {
                Help helpAdd = new Help
                {
                    helpName = help.helpName,
                    helpDescription = help.helpDescription,
                };

                _appDbContext.Help.Add(helpAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal Server Error
            }
        }

        //Update help
        public async Task<int> UpdateHelpAsync(int helpId, HelpViewModal help)
        {
            // Find the object in the db 
            Help attemptToFindInDb = await _appDbContext.Help.FirstOrDefaultAsync(x => x.helpId == helpId);

            if (attemptToFindInDb == null)
            {
                return 404; // Help not found
            }

            attemptToFindInDb.helpName = help.helpName;
            attemptToFindInDb.helpDescription = help.helpDescription;

            _appDbContext.Help.Update(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        //Delete Help
        public async Task<int> DeleteHelpAsync(int helpId)
        {
            // Find the object in the db 
            Help attemptToFindInDb = await _appDbContext.Help.FirstOrDefaultAsync(x => x.helpId == helpId);

            if (attemptToFindInDb == null)
            {
                return 404; // Help not found
            }

            _appDbContext.Help.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search Help   
        public async Task<List<Help>> SearchHelpAsync(string searchString)
        {
            List<Help> helps = await _appDbContext.Help.Where(x => x.helpName.Contains(searchString)).ToListAsync();

            return helps;
        }
    }
}
