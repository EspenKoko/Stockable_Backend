//using Microsoft.AspNetCore.Mvc;
//using Stockable_Backend.Model;
//using Stockable_Backend.ViewModel;
//using Stockable_Backend.Repository.IRepositories;

//namespace Stockable_Backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ErrorLogErrorCodesController : ControllerBase
//    {
//        private readonly IErrorLogErrorCodesRepository _repository;

//        public ErrorLogErrorCodesController(IErrorLogErrorCodesRepository repository)
//        {
//            _repository = repository;
//        }

//        //Create errorLogErrorCodes
//        [HttpPost]
//        [Route("AddErrorLogErrorCodes")]
//        public async Task<IActionResult> AddErrorLogErrorCodes(ErrorLogErrorCodesViewModal errorLogErrorCodes)
//        {
//            try
//            {
//                var result = await _repository.AddErrorLogErrorCodesAsync(errorLogErrorCodes);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK);
//                }
//                else if (result == 404)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add errorLogErrorCodes the Error code , log or printer was not found");
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

//        //Get all errorLogErrorCodeses
//        [HttpGet]
//        [Route("GetAllErrorLogErrorCodeses")]
//        public async Task<IActionResult> GetAllErrorLogErrorCodeses()
//        {
//            try
//            {
//                ErrorLogErrorCodes[] results = await _repository.GetAllErrorLogErrorCodesAsync();
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

//        //Get a errorLogErrorCodes
//        [HttpGet]
//        [Route("GetErrorLogErrorCodes/{printerId}")]
//        public async Task<IActionResult> GetErrorLogErrorCodes(int printerId)
//        {
//            try
//            {
//                ErrorLogErrorCodes result = await _repository.GetErrorLogErrorCodesAsync(printerId);

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

//        // Search errorLogErrorCodes by Name
//        [HttpGet]
//        [Route("SearchErrorLogErrorCodes/{searchString}")]
//        public async Task<IActionResult> SearchErrorLogErrorCodes(string searchString)
//        {
//            try
//            {
//                List<ErrorLogErrorCodes> results = await _repository.SearchErrorLogErrorCodesAsync(searchString);
//                return Ok(results);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Update errorLogErrorCodes
//        [HttpPut]
//        [Route("UpdateErrorLogErrorCodes/{printerId}")]
//        public async Task<IActionResult> UpdateErrorLogErrorCodes(int printerId, ErrorLogErrorCodesViewModal errorLogErrorCodes)
//        {
//            try
//            {
//                var result = await _repository.UpdateErrorLogErrorCodesAsync(printerId, errorLogErrorCodes);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK);
//                }
//                else if (result == 404)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update errorLogErrorCodes. ErrorLogErrorCodes not be found");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update errorLogErrorCodes. The city or the client could not be found");
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }


//        // Delete errorLogErrorCodes
//        [HttpDelete]
//        [Route("DeleteErrorLogErrorCodes/{errorLogErrorCodesId}")]
//        public async Task<IActionResult> DeleteErrorLogErrorCodes(int errorLogErrorCodesId)
//        {
//            try
//            {
//                var result = await _repository.DeleteErrorLogErrorCodesAsync(errorLogErrorCodesId);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted errorLogErrorCodes");
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
