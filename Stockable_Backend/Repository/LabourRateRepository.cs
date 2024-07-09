using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class LabourRateRepository : ILabourRateRepository
    {
        private readonly AppDbContext _appDbContext;

        public LabourRateRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //LabourRate
        //Get LabourRate
        public async Task<LabourRate> GetLabourRateAsync(int labourRateId)
        {
            LabourRate labourRate = await _appDbContext.LabourRates.Where(x => x.labourRateId == labourRateId).FirstOrDefaultAsync();

            return labourRate;
        }

        //Get all LabourRates
        public async Task<LabourRate[]> GetAllLabourRatesAsync()
        {
            IQueryable<LabourRate> query = _appDbContext.LabourRates;
            return await query.ToArrayAsync();
        }

        // Create LabourRate
        public async Task<int> AddLabourRateAsync(LabourRateViewModal labourRate)
        {
            try
            {
                LabourRate labourRateAdd = new LabourRate
                {
                    labourRate = labourRate.labourRate,
                    labourRateDate = labourRate.labourRateDate
                };

                await _appDbContext.LabourRates.AddAsync(labourRateAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update LabourRate
        public async Task<int> UpdateLabourRateAsync(int labourRateId, LabourRateViewModal labourRate)
        {
            try
            {
                LabourRate existingLabourRate = await _appDbContext.LabourRates.FindAsync(labourRateId);
                if (existingLabourRate == null)
                {
                    return 404;
                }

                existingLabourRate.labourRate = labourRate.labourRate;
                existingLabourRate.labourRateDate = labourRate.labourRateDate;

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete LabourRate
        public async Task<int> DeleteLabourRateAsync(int labourRateId)
        {
            LabourRate attemptToFindInDb = await _appDbContext.LabourRates.FindAsync(labourRateId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.LabourRates.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }
    }
}
