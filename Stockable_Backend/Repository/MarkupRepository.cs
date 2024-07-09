using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class MarkupRepository : IMarkupRepository
    {
        private readonly AppDbContext _appDbContext;

        public MarkupRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Markup
        //Get Markup
        public async Task<Markup> GetMarkupAsync(int markupId)
        {
            Markup markup = await _appDbContext.Markups.Where(x => x.markupId == markupId).FirstOrDefaultAsync();

            return markup;
        }

        //Get all Markups
        public async Task<Markup[]> GetAllMarkupsAsync()
        {
            IQueryable<Markup> query = _appDbContext.Markups;
            return await query.ToArrayAsync();
        }

        // Create Markup
        public async Task<int> AddMarkupAsync(MarkupViewModal markup)
        {
            try
            {
                Markup markupAdd = new Markup
                {
                    markupPercent = markup.markupPercent,
                    markupDate = markup.markupDate
                };

                await _appDbContext.Markups.AddAsync(markupAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update Markup
        public async Task<int> UpdateMarkupAsync(int markupId, MarkupViewModal markup)
        {
            try
            {
                Markup existingMarkup = await _appDbContext.Markups.FindAsync(markupId);
                if (existingMarkup == null)
                {
                    return 404;
                }

                existingMarkup.markupPercent = markup.markupPercent;
                existingMarkup.markupDate = markup.markupDate;

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete Markup
        public async Task<int> DeleteMarkupAsync(int markupId)
        {
            Markup attemptToFindInDb = await _appDbContext.Markups.FindAsync(markupId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.Markups.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }
    }
}
