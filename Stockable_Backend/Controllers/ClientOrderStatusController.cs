using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Clientable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrderStatusController : ControllerBase
    {
        private readonly IClientOrderStatusRepository _repository;

        public ClientOrderStatusController(IClientOrderStatusRepository repository)
        {
            _repository = repository;
        }


        //Create ClientOrderStatus
        [HttpPost]
        [Route("AddClientOrderStatus")]
        public async Task<IActionResult> AddClientOrderStatus(ClientOrderStatusViewModal clientOrderStatus)
        {
            try
            {
                var result = await _repository.AddClientOrderStatusAsync(clientOrderStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + clientOrderStatus.clientOrderStatusName);
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


        //Get all ClientOrderStatuss
        [HttpGet]
        [Route("GetAllClientOrderStatuses")]
        public async Task<IActionResult> GetAllClientOrderStatuss()
        {
            try
            {
                ClientOrderStatus[] results = await _repository.GetAllClientOrderStatussAsync();
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

        //Get a ClientOrderStatus
        [HttpGet]
        [Route("GetClientOrderStatus/{clientOrderStatusId}")]
        public async Task<IActionResult> GetClientOrderStatus(int clientOrderStatusId)
        {
            try
            {
                ClientOrderStatus result = await _repository.GetClientOrderStatusAsync(clientOrderStatusId);

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

        // Search ClientOrderStatus by Name
        [HttpGet]
        [Route("SearchClientOrderStatus/{searchString}")]
        public async Task<IActionResult> SearchClientOrderStatus(string searchString)
        {
            try
            {
                List<ClientOrderStatus> results = await _repository.SearchClientOrderStatusAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update ClientOrderStatus
        [HttpPut]
        [Route("UpdateClientOrderStatus/{clientOrderStatusId}")]
        public async Task<IActionResult> UpdateClientOrderStatus(int clientOrderStatusId, ClientOrderStatusViewModal clientOrderStatus)
        {
            try
            {
                var result = await _repository.UpdateClientOrderStatusAsync(clientOrderStatusId, clientOrderStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + clientOrderStatus.clientOrderStatusName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update Client Order Status. Status not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete ClientOrderStatus
        [HttpDelete]
        [Route("DeleteClientOrderStatus/{clientOrderStatusId}")]
        public async Task<IActionResult> DeleteClientOrderStatus(int clientOrderStatusId)
        {

            try
            {
                var result = await _repository.DeleteClientOrderStatusAsync(clientOrderStatusId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Client Order Status");
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
