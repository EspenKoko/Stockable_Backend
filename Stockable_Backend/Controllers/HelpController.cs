using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IHelpRepository _repository;

        public HelpController(IHelpRepository repository)
        {
            _repository = repository;
        }
        //Create Help
        [HttpPost]
        [Route("AddHelp")]
        public async Task<IActionResult> AddHelp(HelpViewModal help)
        {
            try
            {
                var result = await _repository.AddHelpAsync(help);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + help.helpName);
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


        //Get all Helps
        [HttpGet]
        [Route("GetAllHelps")]
        public async Task<IActionResult> GetAllHelps()
        {
            try
            {
                Help[] results = await _repository.GetAllHelpAsync();
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

        //Get a Help
        [HttpGet]
        [Route("GetHelp/{helpId}")]
        public async Task<IActionResult> GetHelp(int helpId)
        {
            try
            {
                Help result = await _repository.GetHelpAsync(helpId);

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

        // Search Help by Name
        [HttpGet]
        [Route("SearchHelp/{searchString}")]
        public async Task<IActionResult> SearchHelp(string searchString)
        {
            try
            {
                List<Help> results = await _repository.SearchHelpAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update Help
        [HttpPut]
        [Route("UpdateHelp/{helpId}")]
        public async Task<IActionResult> UpdateHelp(int helpId, HelpViewModal help)
        {
            try
            {
                var result = await _repository.UpdateHelpAsync(helpId, help);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + help.helpName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update help. Help not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete Help
        [HttpDelete]
        [Route("DeleteHelp/{helpId}")]
        public async Task<IActionResult> DeleteHelp(int helpId)
        {

            try
            {
                var result = await _repository.DeleteHelpAsync(helpId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted help");
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
