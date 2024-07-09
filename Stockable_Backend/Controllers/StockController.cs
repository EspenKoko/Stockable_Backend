using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _repository;

        public StockController(IStockRepository repository)
        {
            _repository = repository;
        }


        // Create Stock
        [HttpPost]
        [Route("AddStock")]
        public async Task<IActionResult> AddStock(/*[FromForm]*/ StockViewModal stock)
        {
            try
            {
                
                var result = await _repository.AddStockAsync(stock);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + stock.stockName);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add stock. Stock type could not be found");
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



        //Get all Stock
        [HttpGet]
        [Route("GetAllStocks")]
        public async Task<IActionResult> GetAllStocks()
        {
            try
            {
                var results = await _repository.GetAllStocksAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get a Stock
        [HttpGet]
        [Route("GetStock/{stockId}")]
        public async Task<IActionResult> GetStock(int stockId)
        {
            try
            {
                var result = await _repository.GetStockAsync(stockId);
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

        // Search Stock by Name
        [HttpGet]
        [Route("SearchStock/{searchString}")]
        public async Task<IActionResult> SearchStock(string searchString)
        {
            try
            {
                var results = await _repository.SearchStockAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        // Update Stock
        [HttpPut]
        [Route("UpdateStock/{stockId}")]
        public async Task<IActionResult> UpdateStock(/*[FromForm]*/ int stockId, StockViewModal stockViewModal)
        {
            try
            {
               
                var result = await _repository.UpdateStockAsync(stockId, stockViewModal);

                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + stockViewModal.stockName);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update stock. Stock item not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update stock. Stock type could not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }



        // Delete Stock
        [HttpDelete]
        [Route("DeleteStock/{stockId}")]
        public async Task<IActionResult> DeleteCity(int stockId)
        {

            try
            {
                var result = await _repository.DeleteStockAsync(stockId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted stock item");
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
