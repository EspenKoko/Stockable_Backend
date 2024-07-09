using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrderStockController : ControllerBase
    {
        private readonly IClientOrderStockRepository _repository;

        public ClientOrderStockController(IClientOrderStockRepository repository)
        {
            _repository = repository;
        }

        //Create clientOrderStock
        [HttpPost]
        [Route("AddClientOrderStock")]
        public async Task<IActionResult> AddClientOrderStock(ClientOrderStockViewModal clientOrderStock)
        {
            try
            {
                var result = await _repository.AddCLientOrderStockAsync(clientOrderStock);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added Order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add stock supplier Order the Supplier or the stock could not be found");
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


        //Get all clientOrderStockes
        [HttpGet]
        [Route("GetAllClientOrderStocks")]
        public async Task<IActionResult> GetAllClientOrderStocks()
        {
            try
            {
                ClientOrderStock[] results = await _repository.GetAllCLientOrderStockAsync();
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

        //Get a clientOrderStock
        [HttpGet]
        [Route("GetClientOrderStock/{clientOrderId}")]
        public async Task<IActionResult> GetClientOrderStock(int clientOrderId)
        {
            try
            {
                List<ClientOrderStock> result = await _repository.GetCLientOrderStockAsync(clientOrderId);

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

        // Search clientOrderStock by Name
        [HttpGet]
        [Route("SearchClientOrderStock/{searchString}")]
        public async Task<IActionResult> SearchClientOrderStock(string searchString)
        {
            try
            {
                List<ClientOrderStock> results = await _repository.SearchCLientOrderStockAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update clientOrderStock
        [HttpPut]
        [Route("UpdateClientOrderStock/{clientOrderId}")]
        public async Task<IActionResult> UpdateClientOrderStock(int clientOrderId, ClientOrderStockViewModal clientOrderStock)
        {
            try
            {
                var result = await _repository.UpdateCLientOrderStockAsync(clientOrderId, clientOrderStock);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated Order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update clientOrderStock. ClientOrderStock not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update clientOrderStock. The  Supplier or the employee or the status could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete clientOrderStock
        [HttpDelete]
        [Route("DeleteClientOrderStock/{clientOrderId}")]
        public async Task<IActionResult> DeleteClientOrderStock(int clientOrderId)
        {
            try
            {
                var result = await _repository.DeleteCLientOrderStockAsync(clientOrderId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted stock Supplier Order");
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

        // Delete clientOrderStock item
        [HttpDelete]
        [Route("DeleteClientOrderStockItem")]
        public async Task<IActionResult> DeleteClientOrderStockItem(int clientOrderId, int stockId)
        {
            try
            {
                var result = await _repository.DeleteCLientOrderStockItemAsync(clientOrderId, stockId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted stock Supplier Order Item");
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
