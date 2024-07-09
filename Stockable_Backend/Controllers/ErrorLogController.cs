using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController :ControllerBase
    {
        private readonly IErrorLogRepository _repository;

        public ErrorLogController(IErrorLogRepository repository)
        {
            _repository = repository;
        }

        //Create errorLog
        [HttpPost]
        [Route("AddErrorLog")]
        public async Task<IActionResult> AddErrorLog(ErrorLogViewModal errorLog)
        {
            try
            {
                var result = await _repository.AddErrorLogAsync(errorLog);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully logged error on this day: " + errorLog.errorlogDate);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add errorLog the Error status or the client could not be found");
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


        //Get all errorLoges
        [HttpGet]
        [Route("GetAllErrorLogs")]
        public async Task<IActionResult> GetAllErrorLogs()
        {
            try
            {
                ErrorLog[] results = await _repository.GetAllErrorLogAsync();
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

        //Get a errorLog
        [HttpGet]
        [Route("GetErrorLog/{errorLogId}")]
        public async Task<IActionResult> GetErrorLog(int errorLogId)
        {
            try
            {
                ErrorLog result = await _repository.GetErrorLogAsync(errorLogId);

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

        // Search errorLog by serlie Number
        [HttpGet]
        [Route("SearchErrorLog/{searchString}")]
        public async Task<IActionResult> SearchErrorLog(string searchString)
        {
            try
            {
                List<ErrorLog> results = await _repository.SearchErrorLogAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update errorLog
        [HttpPut]
        [Route("UpdateErrorLog/{errorLogId}")]
        public async Task<IActionResult> UpdateErrorLog(int errorLogId, ErrorLogViewModal errorLog)
        {
            try
            {
                var result = await _repository.UpdateErrorLogAsync(errorLogId, errorLog);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated error");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update errorLog. ErrorLog not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update errorLog. The Error status or the client could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete errorLog
        [HttpDelete]
        [Route("DeleteErrorLog/{errorLogId}")]
        public async Task<IActionResult> DeleteErrorLog(int errorLogId)
        {
            try
            {
                var result = await _repository.DeleteErrorLogAsync(errorLogId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted errorLog");
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
