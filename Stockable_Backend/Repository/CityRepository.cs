using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class CityRepository: ICityRepository
    {
        private readonly AppDbContext _appDbContext;

        public CityRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //City

        //Get all cities
        public async Task<City[]> GetAllCitiesAsync()
        {
            IQueryable<City> query = _appDbContext.Cities.Include(c => c.province);
            return await query.ToArrayAsync();
        }

        //Get City
        public async Task<City> GetCityAsync(int cityId)
        {
            return await _appDbContext.Cities.Include(c => c.province)
                .FirstOrDefaultAsync(x => x.cityId == cityId);
        }


        //Create City
        public async Task<int> AddCityAsync(CityViewModal city)
        {
            try
            {
                Province province = await _appDbContext.Provinces.FindAsync(city.provinceId);
                if (province != null)
                {
                    City cityAdd = new City
                    {
                        cityName = city.cityName,
                        provinceId = city.provinceId
                    };

                    await _appDbContext.Cities.AddAsync(cityAdd);
                    await _appDbContext.SaveChangesAsync();
                    return 200;
                }

                return 404;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        //Update City
        public async Task<int> UpdateCityAsync(int cityId, CityViewModal city)
        {
            try
            {
                // Find the existing city in the database
                City existingCity = await _appDbContext.Cities.FirstOrDefaultAsync(x => x.cityId == cityId);

                if (existingCity == null) 
                { 
                    return 404; // City not found
                }

                // Find the province associated with the city
                Province province = await _appDbContext.Provinces.FindAsync(city.provinceId);

                if (province == null)
                {
                    return 501; // Invalid province
                }

                // Update the city details
                existingCity.cityName = city.cityName;
                existingCity.provinceId = city.provinceId;

                // Save changes to the database
                _appDbContext.Cities.Update(existingCity);
                await _appDbContext.SaveChangesAsync();

                return 200; // Success
            }
            catch (Exception)
            {
                return 500; // Internal server error
            }
        }



        //Delete City
        public async Task<int> DeleteCityAsync(int cityId)
        {
            City city = await _appDbContext.Cities.FirstOrDefaultAsync(x => x.cityId == cityId);
            if (city == null)
                return 404;

            _appDbContext.Cities.Remove(city);
            await _appDbContext.SaveChangesAsync();
            return 200;
        }

        //Search City   
        public async Task<List<City>> SearchCityAsync(string searchString)
        {
            return await _appDbContext.Cities.Include(c => c.province)
                .Where(x => x.cityName.Contains(searchString))
                .ToListAsync();
        }
    }
}
