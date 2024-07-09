using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTakeController : ControllerBase
    {
        private readonly IStockTakeRepository _repository;

        public StockTakeController(IStockTakeRepository repository)
        {
            _repository = repository;
        }

        // Create stock take
        [HttpPost]
        [Route("AddStockTake")]
        public async Task<IActionResult> AddStockTake(StockTakeViewModal stockTake)
        {
            try
            {
                var result = await _repository.AddStockTakeAsync(stockTake);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added Stock Take");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add stoke take the employee could not be found");
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

        // Get all stock takes
        [HttpGet]
        [Route("GetAllStockTaks")]
        public async Task<IActionResult> GetAllStockTakes()
        {
            try
            {
                StockTake[] results = await _repository.GetAllStockTakesAsync();
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

        // Get a stock take
        [HttpGet]
        [Route("GetStockTake/{stockTakeId}")]
        public async Task<IActionResult> GetStockTake(int stockTakeId)
        {
            try
            {
                StockTake result = await _repository.GetStockTakeAsync(stockTakeId);
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

        // Search stockTake by Name
        [HttpGet]
        [Route("SearchStockTake/{searchString}")]
        public async Task<IActionResult> SearchStockTake(string searchString)
        {
            try
            {
                List<StockTake> results = await _repository.SearchStockTakeAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        // Update stock take
        [HttpPut]
        [Route("UpdateStockTake/{stockTakeId}")]
        public async Task<IActionResult> UpdateStockTake(int stockTakeId, StockTakeViewModal stockTake)
        {
            try
            {
                var result = await _repository.UpdateStockTakeAsync(stockTakeId, stockTake);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated Stock Take");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update stock take. Take not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update stockTake. The employee could not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        // Delete stock take
        [HttpDelete]
        [Route("DeleteStockTake/{stockTakeId}")]
        public async Task<IActionResult> DeleteStockTake(int stockTakeId)
        {
            try
            {
                var result = await _repository.DeleteStockTakeAsync(stockTakeId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted stock take");
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
