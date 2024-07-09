using Stockable_Backend.Repository.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.ViewModel;
//using ClientUserRequestable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientUserRequestController : ControllerBase
    {
        private readonly IClientUserRequestRepository _repository;

        public ClientUserRequestController(IClientUserRequestRepository repository)
        {
            _repository = repository;
        }

        //Create ClientUserRequest
        [HttpPost]
        [Route("AddClientUserRequest")]
        public async Task<IActionResult> AddClientUserRequest(ClientUserRequestViewModal clientUserRequest)
        {
            try
            {
                var result = await _repository.AddClientUserRequestAsync(clientUserRequest);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + clientUserRequest.name);
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


        //Get all clientUserRequests
        [HttpGet]
        [Route("GetAllClientUserRequests")]
        public async Task<IActionResult> GetAllClientUserRequests()
        {
            try
            {
                var results = await _repository.GetAllClientUserRequestsAsync();
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

        //Get a ClientUserRequest
        [HttpGet]
        [Route("GetClientUserRequest/{clientUserRequestId}")]
        public async Task<IActionResult> GetClientUserRequest(int clientUserRequestId)
        {
            try
            {
                var result = await _repository.GetClientUserRequestAsync(clientUserRequestId);
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

        //Update clientUserRequest
        [HttpPut]
        [Route("UpdateClientUserRequest/{clientUserRequestId}")]
        public async Task<IActionResult> UpdateClientUserRequest(int clientUserRequestId, ClientUserRequestViewModal clientUserRequest)
        {
            try
            {
                var result = await _repository.UpdateClientUserRequestAsync(clientUserRequestId, clientUserRequest);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + clientUserRequest.name);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update clientUserRequest. ClientUserRequest not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete clientUserRequest
        [HttpDelete]
        [Route("DeleteClientUserRequest/{clientUserRequestId}")]
        public async Task<IActionResult> DeleteClientUserRequest(int clientUserRequestId)
        {

            try
            {
                var result = await _repository.DeleteClientUserRequestAsync(clientUserRequestId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted clientUserRequest");
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
