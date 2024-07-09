using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _repository;

        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }


        //Create City
        [HttpPost]
        [Route("AddCity")]
        public async Task<IActionResult> AddCity(CityViewModal city)
        {
            try
            {
                var result = await _repository.AddCityAsync(city);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + city.cityName);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add city.The province could not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured, please try again");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        //Get all Cities
        [HttpGet]
        [Route("GetAllCities")]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                City[] results = await _repository.GetAllCitiesAsync();
                if (results.Length == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, results);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Get a City
        [HttpGet]
        [Route("GetCity/{cityId}")]
        public async Task<IActionResult> GetCity(int cityId)
        {
            try
            {
                City result = await _repository.GetCityAsync(cityId);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        // Search City by Name
        [HttpGet]
        [Route("SearchCity/{searchString}")]
        public async Task<IActionResult> SearchCity(string searchString)
        {
            try
            {
                List<City> results = await _repository.SearchCityAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update City
        [HttpPut]
        [Route("UpdateCity/{cityId}")]
        public async Task<IActionResult> UpdateCity(int cityId, CityViewModal city)
        {
            try
            {
                var result = await _repository.UpdateCityAsync(cityId, city);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + city.cityName);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update city. City not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update city. The province could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete City
        [HttpDelete]
        [Route("DeleteCity/{cityId}")]
        public async Task<IActionResult> DeleteCity(int cityId)
        {

            try
            {
                var result = await _repository.DeleteCityAsync(cityId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted city");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }
    }
}
