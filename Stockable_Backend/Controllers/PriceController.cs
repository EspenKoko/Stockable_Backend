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
    public class PriceController : ControllerBase
    {
        private readonly IPriceRepository _repository;

        public PriceController(IPriceRepository repository)
        {
            _repository = repository;
        }


        //Create Price
        [HttpPost]
        [Route("AddPrice")]
        public async Task<IActionResult> AddPrice(PriceViewModal price)
        {
            try
            {
                var result = await _repository.AddPriceAsync(price);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add a the price, The stock item could not be found");
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


        //Get all Prices
        [HttpGet]
        [Route("GetAllPrices")]
        public async Task<IActionResult> GetAllPrices()
        {
            try
            {
                var results = await _repository.GetAllPriceAsync();
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

        //Get a Price
        [HttpGet]
        [Route("GetPrice/{priceId}")]
        public async Task<IActionResult> GetPrice(int priceId)
        {
            try
            {
                var result = await _repository.GetPriceAsync(priceId);
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

        // Search price by stock Name
        [HttpGet]
        [Route("SearchBranch/{searchString}")]
        public async Task<IActionResult> SearchBranch(string searchString)
        {
            try
            {
                List<Price> results = await _repository.SearchPriceAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update Price
        [HttpPut]
        [Route("UpdatePrice/{priceId}")]
        public async Task<IActionResult> UpdatPrice(int priceId, PriceViewModal price)
        {
            try
            {
                var result = await _repository.UpdatePriceAsync(priceId, price);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated the stock item's price to " + price.price);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update the price. price not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update the price. The stock item could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete Price
        [HttpDelete]
        [Route("DeletePrice/{priceId}")]
        public async Task<IActionResult> DeletePrice(int priceId)
        {

            try
            {
                var result = await _repository.DeletePriceAsync(priceId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted the stock items price");
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
