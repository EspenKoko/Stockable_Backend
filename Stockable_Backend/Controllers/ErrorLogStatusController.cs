using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogStatusController : ControllerBase
    {
        private readonly IErrorLogStatusRepository _repository;

        public ErrorLogStatusController(IErrorLogStatusRepository repository)
        {
            _repository = repository;
        }


        //Create ErrorLogStatus
        [HttpPost]
        [Route("AddErrorLogStatus")]
        public async Task<IActionResult> AddErrorLogStatus(ErrorLogStatusViewModal errorLogStatus)
        {
            try
            {
                var result = await _repository.AddErrorLogStatusAsync(errorLogStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + errorLogStatus.errorLogStatusName);
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


        //Get all ErrorLogStatuss
        [HttpGet]
        [Route("GetAllErrorLogStatus")]
        public async Task<IActionResult> GetAllErrorLogStatus()
        {
            try
            {
                ErrorLogStatus[] results = await _repository.GetAllErrorLogStatusAsync();
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

        //Get a ErrorLogStatus
        [HttpGet]
        [Route("GetErrorLogStatus/{errorLogStatusId}")]
        public async Task<IActionResult> GetErrorLogStatus(int errorLogStatusId)
        {
            try
            {
                ErrorLogStatus result = await _repository.GetErrorLogStatusAsync(errorLogStatusId);

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

        // Search ErrorLogStatus by Name
        [HttpGet]
        [Route("SearchErrorLogStatus/{searchString}")]
        public async Task<IActionResult> SearchErrorLogStatus(string searchString)
        {
            try
            {
                List<ErrorLogStatus> results = await _repository.SearchErrorLogStatusAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update ErrorLogStatus
        [HttpPut]
        [Route("UpdateErrorLogStatus/{errorLogStatusId}")]
        public async Task<IActionResult> UpdateErrorLogStatus(int errorLogStatusId, ErrorLogStatusViewModal errorLogStatus)
        {
            try
            {
                var result = await _repository.UpdateErrorLogStatusAsync(errorLogStatusId, errorLogStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + errorLogStatus.errorLogStatusName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update branch. Branch not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete ErrorLogStatus
        [HttpDelete]
        [Route("DeleteErrorLogStatus/{errorLogStatusId}")]
        public async Task<IActionResult> DeleteErrorLogStatus(int errorLogStatusId)
        {

            try
            {
                var result = await _repository.DeleteErrorLogStatusAsync(errorLogStatusId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted status");
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
