using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorCodeController : ControllerBase
    {
        private readonly IErrorCodeRepository _repository;

        public ErrorCodeController(IErrorCodeRepository repository)
        {
            _repository = repository;
        }

        // Create ErrorCode
        [HttpPost]
        [Route("AddErrorCode")]
        public async Task<IActionResult> AddErrorCode(ErrorCodeViewModal errorCode)
        {
            try
            {
                var result = await _repository.AddErrorCodeAsync(errorCode);

                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + errorCode.errorCodeName);
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

        // Get all ErrorCodes
        [HttpGet]
        [Route("GetAllErrorCodes")]
        public async Task<IActionResult> GetAllErrorCodes()
        {
            try
            {
                ErrorCode[] results = await _repository.GetAllErrorCodesAsync();
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

        // Get a ErrorCode
        [HttpGet]
        [Route("GetErrorCode/{errorCodeId}")]
        public async Task<IActionResult> GetErrorCode(int errorCodeId)
        {
            try
            {
                ErrorCode result = await _repository.GetErrorCodeAsync(errorCodeId);

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

        // Search ErrorCode by Name
        [HttpGet]
        [Route("SearchErrorCode/{searchString}")]
        public async Task<IActionResult> SearchErrorCode(string searchString)
        {
            try
            {
                List<ErrorCode> results = await _repository.SearchErrorCodeAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        // Update ErrorCode
        [HttpPut]
        [Route("UpdateErrorCode/{errorCodeId}")]
        public async Task<IActionResult> UpdateErrorCode(int errorCodeId, ErrorCodeViewModal errorCode)
        {
            try
            {
                var result = await _repository.UpdateErrorCodeAsync(errorCodeId, errorCode);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + errorCode.errorCodeName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update errorCode. ErrorCode not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        // Delete ErrorCode
        [HttpDelete]
        [Route("DeleteErrorCode/{errorCodeId}")]
        public async Task<IActionResult> DeleteErrorCode(int errorCodeId)
        {
            try
            {
                var result = await _repository.DeleteErrorCodeAsync(errorCodeId);

                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted errorCode");
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
