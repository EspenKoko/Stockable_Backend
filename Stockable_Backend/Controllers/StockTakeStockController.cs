using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTakeStockController : ControllerBase
    {
        private readonly IStockTakeStockRepository _repository;

        public StockTakeStockController(IStockTakeStockRepository repository)
        {
            _repository = repository;
        }

        //Create stockTakeStock
        [HttpPost]
        [Route("AddStockTakeStock")]
        public async Task<IActionResult> AddStockTakeStock(StockTakeStockViewModal stockTakeStock)
        {
            try
            {
                var result = await _repository.AddStockTakeStockAsync(stockTakeStock);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added Order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add stock take stock: The Stock Take or the stock could not be found");
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


        //Get all stockTakeStockes
        [HttpGet]
        [Route("GetAllStockTakeStockes")]
        public async Task<IActionResult> GetAllStockTakeStockes()
        {
            try
            {
                StockTakeStock[] results = await _repository.GetAllStockTakeStocksAsync();
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

        //Get a stockTakeStock
        [HttpGet]
        [Route("GetStockTakeStock/{stockId}")]
        public async Task<IActionResult> GetStockTakeStock(int stockId)
        {
            try
            {
                StockTakeStock result = await _repository.GetStockTakeStockAsync(stockId);

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

        // Search stockTakeStock by Name
        [HttpGet]
        [Route("SearchStockTakeStock/{searchString}")]
        public async Task<IActionResult> SearchStockTakeStock(string searchString)
        {
            try
            {
                List<StockTakeStock> results = await _repository.SearchStockTakeStockAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update stockTakeStock
        [HttpPut]
        [Route("UpdateStockTakeStock/{stockId}")]
        public async Task<IActionResult> UpdateStockTakeStock(int stockId, StockTakeStockViewModal stockTakeStock)
        {
            try
            {
                var result = await _repository.UpdateStockTakeStockAsync(stockId, stockTakeStock);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated stock take");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update stock Take Stock. Stock Take Stock not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update stockTakeStock. The Stock Take or the stock could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete stockTakeStock
        [HttpDelete]
        [Route("DeleteStockTakeStock/{stockId}")]
        public async Task<IActionResult> DeleteStockTakeStock(int stockId)
        {
            try
            {
                var result = await _repository.DeleteStockTakeStockAsync(stockId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted stockTakeStock");
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
