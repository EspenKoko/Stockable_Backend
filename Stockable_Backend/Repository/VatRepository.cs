using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class VatRepository : IVatRepository
    {

        private readonly AppDbContext _appDbContext;

        public VatRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Vat
        //Get Vat
        public async Task<VAT> GetVatAsync(int vatId)
        {
            VAT vat = await _appDbContext.Vats.Where(x => x.vatId == vatId).FirstOrDefaultAsync();

            return vat;
        }

        //Get all Vats
        public async Task<VAT[]> GetAllVatsAsync()
        {
            IQueryable<VAT> query = _appDbContext.Vats;
            return await query.ToArrayAsync();
        }

        // Create Vat
        public async Task<int> AddVatAsync(VatViewModal vat)
        {
            try
            {
                VAT vatAdd = new VAT
                {
                    vatPercent = vat.vatPercent,
                    vatDate = vat.vatDate
                };

                await _appDbContext.Vats.AddAsync(vatAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update Vat
        public async Task<int> UpdateVatAsync(int vatId, VatViewModal vat)
        {
            try
            {
                VAT existingVat = await _appDbContext.Vats.FindAsync(vatId);
                if (existingVat == null)
                {
                    return 404;
                }

                existingVat.vatPercent = vat.vatPercent;
                existingVat.vatDate = vat.vatDate;

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete Vat
        public async Task<int> DeleteVatAsync(int vatId)
        {
            VAT attemptToFindInDb = await _appDbContext.Vats.FindAsync(vatId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.Vats.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

    }
}
