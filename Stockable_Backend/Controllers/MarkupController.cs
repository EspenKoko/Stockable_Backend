using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkupController : ControllerBase
    {
        private readonly IMarkupRepository _repository;

        public MarkupController(IMarkupRepository repository)
        {
            _repository = repository;
        }

        //Create Markup
        [HttpPost]
        [Route("AddMarkup")]
        public async Task<IActionResult> AddMarkup(MarkupViewModal markup)
        {
            try
            {
                var result = await _repository.AddMarkupAsync(markup);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + markup.markupPercent);
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


        //Get all markups
        [HttpGet]
        [Route("GetAllMarkups")]
        public async Task<IActionResult> GetAllMarkups()
        {
            try
            {
                var results = await _repository.GetAllMarkupsAsync();
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

        //Get a Markup
        [HttpGet]
        [Route("GetMarkup/{markupId}")]
        public async Task<IActionResult> GetMarkup(int markupId)
        {
            try
            {
                var result = await _repository.GetMarkupAsync(markupId);
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

        //Update markup
        [HttpPut]
        [Route("UpdateMarkup/{markupId}")]
        public async Task<IActionResult> UpdateMarkup(int markupId, MarkupViewModal markup)
        {
            try
            {
                var result = await _repository.UpdateMarkupAsync(markupId, markup);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + markup.markupPercent);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update markup. Markup not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete markup
        [HttpDelete]
        [Route("DeleteMarkup/{markupId}")]
        public async Task<IActionResult> DeleteMarkup(int markupId)
        {

            try
            {
                var result = await _repository.DeleteMarkupAsync(markupId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted markup entry");
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
