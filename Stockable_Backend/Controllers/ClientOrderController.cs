using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Clientable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrderController : ControllerBase
    {
        private readonly IClientOrderRepository _repository;

        public ClientOrderController(IClientOrderRepository repository)
        {
            _repository = repository;
        }

        //Create clientOrder
        [HttpPost]
        [Route("AddClientOrder")]
        public async Task<IActionResult> AddClientOrder(ClientOrderViewModal clientOrder)
        {
            try
            {
                var result = await _repository.AddClientOrderAsync(clientOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added Client order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add Client Order: Client user or Order status or Client Invoice or Payment Type not found");
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


        //Get all clientOrderes
        [HttpGet]
        [Route("GetAllClientOrders")]
        public async Task<IActionResult> GetAllClientOrderes()
        {
            try
            {
                ClientOrder[] results = await _repository.GetAllClientOrderAsync();
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

        //Get a clientOrder
        [HttpGet]
        [Route("GetClientOrder/{clientOrderId}")]
        public async Task<IActionResult> GetClientOrder(int clientOrderId)
        {
            try
            {
                ClientOrder result = await _repository.GetClientOrderAsync(clientOrderId);

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

        // Search clientOrder by Name
        [HttpGet]
        [Route("SearchClientOrder/{searchString}")]
        public async Task<IActionResult> SearchClientOrder(string searchString)
        {
            try
            {
                List<ClientOrder> results = await _repository.SearchClientOrderAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update clientOrder
        [HttpPut]
        [Route("UpdateClientOrder/{clientOrderId}")]
        public async Task<IActionResult> UpdateClientOrder(int clientOrderId, ClientOrderViewModal clientOrder)
        {
            try
            {
                var result = await _repository.UpdateClientOrderAsync(clientOrderId, clientOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated client order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update client Order. Client Order not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update clientOrder. The Client user or Order status or Client Invoice or Payment Type not found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete clientOrder
        [HttpDelete]
        [Route("DeleteClientOrder/{clientOrderId}")]
        public async Task<IActionResult> DeleteClientOrder(int clientOrderId)
        {
            try
            {
                var result = await _repository.DeleteClientOrderAsync(clientOrderId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted client Order");
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
