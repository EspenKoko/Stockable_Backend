using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceRepository _repository;

        public ProvinceController(IProvinceRepository repository)
        {
            _repository = repository;
        }


        //Create Province
        [HttpPost]
        [Route("AddProvince")]
        public async Task<IActionResult> AddProvince(ProvinceViewModal province)
        {
            try
            {
                var result = await _repository.AddProvinceAsync(province);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + province.provinceName);
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


        //Get all Provinces
        [HttpGet]
        [Route("GetAllProvinces")]
        public async Task<IActionResult> GetAllProvinces()
        {
            try
            {
                Province[] results = await _repository.GetAllProvincesAsync();
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

        //Get a Province
        [HttpGet]
        [Route("GetProvince/{provinceId}")]
        public async Task<IActionResult> GetProvince(int provinceId)
        {
            try
            {
                Province result = await _repository.GetProvinceAsync(provinceId);

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

        // Search Province by Name
        [HttpGet]
        [Route("SearchProvince/{searchString}")]
        public async Task<IActionResult> SearchProvince(string searchString)
        {
            try
            {
                List<Province> results = await _repository.SearchProvinceAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update Province
        [HttpPut]
        [Route("UpdateProvince/{provinceId}")]
        public async Task<IActionResult> UpdateProvince(int provinceId, ProvinceViewModal province)
        {
            try
            {
                var result = await _repository.UpdateProvinceAsync(provinceId, province);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + province.provinceName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update province. Province not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete Province
        [HttpDelete]
        [Route("DeleteProvince/{provinceId}")]
        public async Task<IActionResult> DeleteProvince(int provinceId)
        {

            try
            {
                var result = await _repository.DeleteProvinceAsync(provinceId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted province");
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
