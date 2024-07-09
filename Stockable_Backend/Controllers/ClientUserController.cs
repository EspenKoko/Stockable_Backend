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
    public class ClientUserController : ControllerBase
    {
        private readonly IClientUserRepository _repository;

        public ClientUserController(IClientUserRepository repository)
        {
            _repository = repository;
        }


        //Create ClientUser
        [HttpPost]
        [Route("AddClientUser")]
        public async Task<IActionResult> AddClientUser(ClientUserViewModal clientUser)
        {
            try
            {
                var result = await _repository.AddClientUserAsync(clientUser);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added client user" );
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add client user the user or the client or the branch could not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured, please try again");
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        //Get all ClientUsers
        [HttpGet]
        [Route("GetAllClientUsers")]
        public async Task<IActionResult> GetAllClientUsers()
        {
            try
            {
                ClientUser[] results = await _repository.GetAllClientUsersAsync();
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

        //Get a ClientUser
        [HttpGet]
        [Route("GetClientUser/{clientUserId}")]
        public async Task<IActionResult> GetClientUser(int clientUserId)
        {
            try
            {
                ClientUser result = await _repository.GetClientUserAsync(clientUserId);
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

        // Search ClientUser by Name
        [HttpGet]
        [Route("SearchClientUser/{searchString}")]
        public async Task<IActionResult> SearchClientUser(string searchString)
        {
            try
            {
                List<ClientUser> results = await _repository.SearchClientUserAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update ClientUser
        [HttpPut]
        [Route("UpdateClientUser/{clientUserId}")]
        public async Task<IActionResult> UpdateClientUser(int clientUserId, ClientUserViewModal clientUser)
        {
            try
            {
                var result = await _repository.UpdateClientUserAsync(clientUserId, clientUser);

                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated client user" );
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update ClientUser. ClientUser not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update ClientUser. The user or the client or the branch could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete ClientUser
        [HttpDelete]
        [Route("DeleteClientUser/{clientUserId}")]
        public async Task<IActionResult> DeleteClientUser(int clientUserId)
        {

            try
            {
                var result = await _repository.DeleteClientUserAsync(clientUserId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted ClientUser");
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
