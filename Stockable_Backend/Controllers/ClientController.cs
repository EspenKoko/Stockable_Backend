using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Model;
using Stockable_Backend.Repository;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientRepository _repository;

        public ClientController(IClientRepository repository)
        {
            _repository = repository;
        }


        //Create Client
        [HttpPost]
        [Route("AddClient")]
        public async Task<IActionResult> AddClient(ClientViewModal client)
        {
            try
            {
                var result = await _repository.AddClientAsync(client);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + client.clientName);
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


        //Get all clients
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                Client[] results = await _repository.GetAllClientsAsync();
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

        //Get a client
        [HttpGet]
        [Route("GetClient/{clientId}")]
        public async Task<IActionResult> GetClient(int clientId)
        {
            try
            {
                Client result = await _repository.GetClientAsync(clientId);
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

        // Search client
        [HttpGet]
        [Route("SearchClient/{searchString}")]
        public async Task<IActionResult> SearchClient(string searchString)
        {
            try
            {
                List<Client> results = await _repository.SearchClientAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update branch
        [HttpPut]
        [Route("UpdateCLient/{clientId}")]
        public async Task<IActionResult> UpdateCourse(int clientId, ClientViewModal client)
        {
            try
            {
                var result = await _repository.UpdateClientAsync(clientId, client);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + client.clientName);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update branch. Branch not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update branch. The city or the client could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        //Delete Client
        [HttpDelete]
        [Route("DeleteClient/{clientId}")]
        public async Task<IActionResult> DeleteCourse(int clientId)
        {

            try
            {
                var result = await _repository.DeleteClientAsync(clientId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Client");
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
