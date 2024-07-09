using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairStockController : ControllerBase
    {
        private readonly IRepairStockRepository _repository;

        public RepairStockController(IRepairStockRepository repository)
        {
            _repository = repository;
        }

        //Create repairStock
        [HttpPost]
        [Route("AddRepairStock")]
        public async Task<IActionResult> AddRepairStock(RepairStockViewModal repairStock)
        {
            try
            {
                var result = await _repository.AddRepairStockAsync(repairStock);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add Repair Stock: Stock or Repair was not found");
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

        //Get all repairStockes
        [HttpGet]
        [Route("GetAllRepairStock")]
        public async Task<IActionResult> GetAllRepairStockes()
        {
            try
            {
                RepairStock[] results = await _repository.GetAllRepairStockAsync();
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

        //Get a repairStock
        [HttpGet]
        [Route("GetRepairStock/{repairId}")]
        public async Task<IActionResult> GetRepairStock(int repairId)
        {
            try
            {
                List<RepairStock> result = await _repository.GetRepairStockAsync(repairId);

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

        // Search repairStock by Name
        [HttpGet]
        [Route("SearchRepairStock/{searchString}")]
        public async Task<IActionResult> SearchRepairStock(string searchString)
        {
            try
            {
                List<RepairStock> results = await _repository.SearchRepairStockAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update repairStock
        [HttpPut]
        [Route("UpdateRepairStock/{repairId}")]
        public async Task<IActionResult> UpdateRepairStock(int repairId, RepairStockViewModal repairStock)
        {
            try
            {
                var result = await _repository.UpdateRepairStockAsync(repairId, repairStock);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update Repair Stock. Repair stock not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update Repair Stock. The Stock or Repair could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete repairStock
        [HttpDelete]
        [Route("DeleteRepairStock/{repairId}")]
        public async Task<IActionResult> DeleteRepairStock(int repairId)
        {
            try
            {
                var result = await _repository.DeleteRepairStockAsync(repairId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Repair Stock");
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
