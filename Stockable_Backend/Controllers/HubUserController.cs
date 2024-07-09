//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Stockable_Backend.Model;
//using Stockable_Backend.Repository;
//using Stockable_Backend.Repository.IRepositories;
//using Stockable_Backend.ViewModel;

//namespace Stockable_Backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class HubUserController : ControllerBase
//    {
//        private readonly IHubUserRepository _repository;

//        public HubUserController(IHubUserRepository repository)
//        {
//            _repository = repository;
//        }


//        //Create HubUser
//        [HttpPost]
//        [Route("AddHubUser")]
//        public async Task<IActionResult> AddHubUser(HubUserViewModal hubUser)
//        {
//            try
//            {
//                var result = await _repository.AddHubUserAsync(hubUser);
//                var fullname = hubUser.hubUserName + " " + hubUser.hubUserSurname;
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + fullname);
//                }
//                else if (result == 404)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add hubUser the user or the hub could not be found");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status500InternalServerError);
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }


//        //Get all HubUsers
//        [HttpGet]
//        [Route("GetAllHubUsers")]
//        public async Task<IActionResult> GetAllHubUsers()
//        {
//            try
//            {
//                HubUser[] results = await _repository.GetAllHubUserAsync();
//                if (results.Length == 0)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status200OK, results);
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Get a HubUser
//        [HttpGet]
//        [Route("GetHubUser/{hubUserId}")]
//        public async Task<IActionResult> GetHubUser(int hubUserId)
//        {
//            try
//            {
//                HubUser result = await _repository.GetHubUserAsync(hubUserId);
//                if (result == null)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status200OK, result);
//                }
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        // Search HubUser by Name
//        [HttpGet]
//        [Route("SearchHubUser/{searchString}")]
//        public async Task<IActionResult> SearchHubUser(string searchString)
//        {
//            try
//            {
//                var results = await _repository.SearchHubUserAsync(searchString);
//                return Ok(results);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Update HubUser
//        [HttpPut]
//        [Route("UpdateHubUser/{hubUserId}")]
//        public async Task<IActionResult> UpdateHubUser(int hubUserId, HubUserViewModal hubUser)
//        {
//            try
//            {
//                var result = await _repository.UpdateHubUserAsync(hubUserId, hubUser);
//                var fullname = hubUser.hubUserName + " " + hubUser.hubUserSurname;
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + fullname);
//                }
//                else if (result == 404)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update hubuser. HubUser not be found");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update hubuser. The user or the hub could not be found");
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }


//        // Delete HubUser
//        [HttpDelete]
//        [Route("DeleteHubUser/{hubUserId}")]
//        public async Task<IActionResult> DeleteHubUser(int hubUserId)
//        {

//            try
//            {
//                var result = await _repository.DeleteHubUserAsync(hubUserId);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted branch");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }
//    }
//}
