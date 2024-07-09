using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly IRepairRepository _repository;

        public RepairController(IRepairRepository repository)
        {
            _repository = repository;
        }

        //Create repair
        [HttpPost]
        [Route("AddRepair")]
        public async Task<IActionResult> AddRepair(RepairViewModal repair)
        {
            try
            {
                var result = await _repository.AddRepairAsync(repair);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added repair" + repair.errorLogId);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add repair the ErrorLog or RepairStatus or Printer or Employee could not be found");
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


        //Get all repairs
        [HttpGet]
        [Route("GetAllRepairs")]
        public async Task<IActionResult> GetAllRepairs()
        {
            try
            {
                Repair[] results = await _repository.GetAllRepairAsync();
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

        //Get a repair
        [HttpGet]
        [Route("GetRepair/{repairId}")]
        public async Task<IActionResult> GetRepair(int repairId)
        {
            try
            {
                Repair result = await _repository.GetRepairAsync(repairId);

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

        // Search repair by Name
        [HttpGet]
        [Route("SearchRepair/{searchString}")]
        public async Task<IActionResult> SearchRepair(string searchString)
        {
            try
            {
                List<Repair> results = await _repository.SearchRepairAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update repair
        [HttpPut]
        [Route("UpdateRepair/{repairId}")]
        public async Task<IActionResult> UpdateRepair(int repairId, RepairViewModal repair)
        {
            try
            {
                var result = await _repository.UpdateRepairAsync(repairId, repair);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + repair.errorLogId);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update repair. Repair not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update repair. The ErrorLog or RepairStatus or Printer or Employee could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete repair
        [HttpDelete]
        [Route("DeleteRepair/{repairId}")]
        public async Task<IActionResult> DeleteRepair(int repairId)
        {
            try
            {
                var result = await _repository.DeleteRepairAsync(repairId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted repair");
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
