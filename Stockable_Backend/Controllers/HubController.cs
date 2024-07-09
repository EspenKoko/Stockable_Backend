//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Stockable_Backend.ViewModel;
//using Stockable_Backend.Model;
//using Stockable_Backend.Repository;
//using Stockable_Backend.Repository.IRepositories;

//namespace Stockable_Backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class HubController : ControllerBase
//    {
//        private readonly IHubRepository _repository;

//        public HubController(IHubRepository repository)
//        {
//            _repository = repository;
//        }


//        //Create hub
//        [HttpPost]
//        [Route("AddHub")]
//        public async Task<IActionResult> AddHub(HubViewModal hub)
//        {
//            try
//            {
//                var results = await _repository.AddHubAsync(hub);
//                if (results == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + hub.hubName);
//                }
//                else if (results == 501)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add hub the city could not be found");
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


//        //Get all hub
//        [HttpGet]
//        [Route("GetAllHubs")]
//        public async Task<IActionResult> GetAllHubs()
//        {
//            try
//            {
//                Hub[] results = await _repository.GetAllHubsAsync();
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

//        //Get a hub
//        [HttpGet]
//        [Route("GetHub/{hubId}")]
//        public async Task<IActionResult> GetHub(int hubId)
//        {
//            try
//            {
//                Hub result = await _repository.GetHubAsync(hubId);
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

//        // Search Hub by Name
//        [HttpGet]
//        [Route("SearchHub/{searchString}")]
//        public async Task<IActionResult> SearchHub(string searchString)
//        {
//            try
//            {
//                List<Hub> results = await _repository.SearchHubAsync(searchString);
//                return Ok(results);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Update hub
//        [HttpPut]
//        [Route("UpdateHub/{hubId}")]
//        public async Task<IActionResult> UpdateHub(int hubId, HubViewModal hub)
//        {
//            try
//            {
//                var result = await _repository.UpdateHubAsync(hubId, hub);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + hub.hubName);
//                }
//                else if (result == 404)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update hub. Hub not be found");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update hub. The city could not be found");
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }


//        // Delete hub
//        [HttpDelete]
//        [Route("DeleteHub/{hubId}")]
//        public async Task<IActionResult> DeleteHub(int hubId)
//        {

//            try
//            {
//                var result = await _repository.DeleteHubAsync(hubId);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted hub");
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
