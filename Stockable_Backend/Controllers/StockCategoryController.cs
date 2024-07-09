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
    public class StockCategoryController : ControllerBase
    {
        private readonly IStockCategoryRepository _repository;

        public StockCategoryController(IStockCategoryRepository repository)
        {
            _repository = repository;
        }


        //Create StockCategory
        [HttpPost]
        [Route("AddStockCategory")]
        public async Task<IActionResult> AddStockCategory(StockCategoryViewModal stockCategory)
        {
            try
            {
                var result = await _repository.AddStockCategoryAsync(stockCategory);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + stockCategory.stockCategoryName);
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


        //Get all StockCategories
        [HttpGet]
        [Route("GetAllStockCategories")]
        public async Task<IActionResult> GetAllStockCategories()
        {
            try
            {
                var results = await _repository.GetAllStockCategoriesAsync();
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

        //Get a StockCategory
        [HttpGet]
        [Route("GetStockCategory/{stockCategoryId}")]
        public async Task<IActionResult> GetStockCategory(int stockCategoryId)
        {
            try
            {
                var result = await _repository.GetStockCategoryAsync(stockCategoryId);
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

        // Search StockCategory by Name
        [HttpGet]
        [Route("SearchStockCategory/{searchString}")]
        public async Task<IActionResult> SearchStockCategory(string searchString)
        {
            try
            {
                var results = await _repository.SearchStockCategoryAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update StockCategory
        [HttpPut]
        [Route("UpdateStockCategory/{stockCategoryId}")]
        public async Task<IActionResult> UpdateStockCategory(int stockCategoryId, StockCategoryViewModal stockCategory)
        {
            try
            {
                var result = await _repository.UpdateStockCategoryAsync(stockCategoryId, stockCategory);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + stockCategory.stockCategoryName);
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


        // Delete StockCategory
        [HttpDelete]
        [Route("DeleteStockCategory/{stockCategoryId}")]
        public async Task<IActionResult> DeleteStockCategory(int stockCategoryId)
        {

            try
            {
                var result = await _repository.DeleteStockCategoryAsync(stockCategoryId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Stock category");
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
