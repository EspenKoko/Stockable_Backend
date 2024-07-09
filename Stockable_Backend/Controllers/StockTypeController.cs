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
    public class StockTypeController : ControllerBase
    {
        private readonly IStockTypeRepository _repository;

        public StockTypeController(IStockTypeRepository repository)
        {
            _repository = repository;
        }


        //Create StockType
        [HttpPost]
        [Route("AddStockType")]
        public async Task<IActionResult> AddStockType(StockTypeViewModal stockType)
        {
            try
            {
                var result = await _repository.AddStockTypeAsync(stockType);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + stockType.stockTypeName);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add stock type. Stock category could not be found");
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


        //Get all stockTypes
        [HttpGet]
        [Route("GetStockTypes")]
        public async Task<IActionResult> GetAllStockTypes()
        {
            try
            {
                var results = await _repository.GetAllStockTypesAsync();
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

        //Get a StockType
        [HttpGet]
        [Route("GetStockType/{stockTypeId}")]
        public async Task<IActionResult> GetStockType(int stockTypeId)
        {
            try
            {
                var result = await _repository.GetStockTypeAsync(stockTypeId);
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

        // Search StockType by Name
        [HttpGet]
        [Route("SearchStockType/{searchString}")]
        public async Task<IActionResult> SearchStockType(string searchString)
        {
            try
            {
                var results = await _repository.SearchStockTypeAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update StockType
        [HttpPut]
        [Route("UpdateStockType/{stockTypeId}")]
        public async Task<IActionResult> UpdateStockType(int stockTypeId, StockTypeViewModal stockType)
        {
            try
            {
                var result = await _repository.UpdateStockTypeAsync(stockTypeId, stockType);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + stockType.stockTypeName );
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed tostock type. Type not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to add stock type. Stock category could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete StockType
        [HttpDelete]
        [Route("DeleteStockType/{stockTypeId}")]
        public async Task<IActionResult> DeleteStockType(int stockTypeId)
        {

            try
            {
                var result = await _repository.DeleteStockTypeAsync(stockTypeId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted stock type");
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
