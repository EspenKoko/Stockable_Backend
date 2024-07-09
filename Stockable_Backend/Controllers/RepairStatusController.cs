using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairStatusController : ControllerBase
    {
        private readonly IRepairStatusRepository _repository;

        public RepairStatusController(IRepairStatusRepository repository)
        {
            _repository = repository;
        }

        //Create RepairStatus
        [HttpPost]
        [Route("AddRepairStatus")]
        public async Task<IActionResult> AddRepairStatus(RepairStatusViewModal repairStatus)
        {
            try
            {
                var result = await _repository.AddRepairStatusAsync(repairStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + repairStatus.repairStatusName);
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


        //Get all RepairStatus
        [HttpGet]
        [Route("GetAllRepairStatus")]
        public async Task<IActionResult> GetAllRepairStatuss()
        {
            try
            {
                RepairStatus[] results = await _repository.GetAllRepairStatusAsync();
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

        //Get a RepairStatus
        [HttpGet]
        [Route("GetRepairStatus/{repairStatusId}")]
        public async Task<IActionResult> GetRepairStatus(int repairStatusId)
        {
            try
            {
                RepairStatus result = await _repository.GetRepairStatusAsync(repairStatusId);

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

        // Search RepairStatus by Name
        [HttpGet]
        [Route("SearchRepairStatus/{searchString}")]
        public async Task<IActionResult> SearchRepairStatus(string searchString)
        {
            try
            {
                List<RepairStatus> results = await _repository.SearchRepairStatusAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update RepairStatus
        [HttpPut]
        [Route("UpdateRepairStatus/{repairStatusId}")]
        public async Task<IActionResult> UpdateRepairStatus(int repairStatusId, RepairStatusViewModal repairStatus)
        {
            try
            {
                var result = await _repository.UpdateRepairStatusAsync(repairStatusId, repairStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + repairStatus.repairStatusName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update OrderStatus. OrderStatus not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete RepairStatus
        [HttpDelete]
        [Route("DeleteRepairStatus/{repairStatusId}")]
        public async Task<IActionResult> DeleteRepairStatus(int repairStatusId)
        {

            try
            {
                var result = await _repository.DeleteRepairStatusAsync(repairStatusId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted OrderStatus");
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
