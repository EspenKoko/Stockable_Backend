using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProvinceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Province
        //Get all provinces
        public async Task<Province[]> GetAllProvincesAsync()
        {
            IQueryable<Province> query = _appDbContext.Provinces;
            return await query.ToArrayAsync();
        }

        //Get province
        public async Task<Province> GetProvinceAsync(int provinceId)
        {
            Province province = await _appDbContext.Provinces.FirstOrDefaultAsync(x => x.provinceId == provinceId);

            return province;
        }


        //Create province
        public async Task<int> AddProvinceAsync(ProvinceViewModal province)
        {
            try
            {
                Province provinceAdd = new Province
                {
                    provinceName = province.provinceName
                };

                _appDbContext.Provinces.Add(provinceAdd);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal Server Error
            }
        }

        //Update province
        public async Task<int> UpdateProvinceAsync(int provinceId, ProvinceViewModal province)
        {
            // Find the object in the db 
            Province attemptToFindInDb = await _appDbContext.Provinces.FirstOrDefaultAsync(x => x.provinceId == provinceId);

            if (attemptToFindInDb == null)
            {
                return 404; // Province not found
            }

            attemptToFindInDb.provinceName = province.provinceName;

            _appDbContext.Provinces.Update(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }


        //Delete Province
        public async Task<int> DeleteProvinceAsync(int provinceId)
        {
            // Find the object in the db 
            Province attemptToFindInDb = await _appDbContext.Provinces.FirstOrDefaultAsync(x => x.provinceId == provinceId);

            if (attemptToFindInDb == null)
            {
                return 404; // Province not found
            }
           
            _appDbContext.Provinces.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200; // Success
        }

        //Search Province   
        public async Task<List<Province>> SearchProvinceAsync(string searchString)
        {
            List<Province> provinces = await _appDbContext.Provinces.Where(x => x.provinceName.Contains(searchString)).ToListAsync();

            return provinces;
        }
    }
}
