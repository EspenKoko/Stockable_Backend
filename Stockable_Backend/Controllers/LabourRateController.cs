using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabourRateController : ControllerBase
    {
        private readonly ILabourRateRepository _repository;

        public LabourRateController(ILabourRateRepository repository)
        {
            _repository = repository;
        }

        //Create LabourRate
        [HttpPost]
        [Route("AddLabourRate")]
        public async Task<IActionResult> AddLabourRate(LabourRateViewModal labourRate)
        {
            try
            {
                var result = await _repository.AddLabourRateAsync(labourRate);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + labourRate.labourRate);
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


        //Get all labourRates
        [HttpGet]
        [Route("GetAllLabourRates")]
        public async Task<IActionResult> GetAllLabourRates()
        {
            try
            {
                var results = await _repository.GetAllLabourRatesAsync();
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

        //Get a LabourRate
        [HttpGet]
        [Route("GetLabourRate/{labourRateId}")]
        public async Task<IActionResult> GetLabourRate(int labourRateId)
        {
            try
            {
                var result = await _repository.GetLabourRateAsync(labourRateId);
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

        //Update labourRate
        [HttpPut]
        [Route("UpdateLabourRate/{labourRateId}")]
        public async Task<IActionResult> UpdateLabourRate(int labourRateId, LabourRateViewModal labourRate)
        {
            try
            {
                var result = await _repository.UpdateLabourRateAsync(labourRateId, labourRate);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + labourRate.labourRate);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update labourRate. LabourRate not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete labourRate
        [HttpDelete]
        [Route("DeleteLabourRate/{labourRateId}")]
        public async Task<IActionResult> DeleteLabourRate(int labourRateId)
        {

            try
            {
                var result = await _repository.DeleteLabourRateAsync(labourRateId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted labourRate entry");
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
