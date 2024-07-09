using Stockable_Backend.Repository.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Model;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransitPrinterController : ControllerBase
    {
        private readonly ITransitPrinterRepository _repository;

        public TransitPrinterController(ITransitPrinterRepository repository)
        {
            _repository = repository;
        }

        //Create TransitPrinter
        [HttpPost]
        [Route("AddTransitPrinter")]
        public async Task<IActionResult> AddTransitPrinter(TransitPrinterViewModal TransitPrinter)
        {
            try
            {
                var result = await _repository.AddTransitPrinterAsync(TransitPrinter);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + TransitPrinter.technicianId);
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


        //Get all TransitPrinters
        [HttpGet]
        [Route("GetAllTransitPrinters")]
        public async Task<IActionResult> GetAllTransitPrinters()
        {
            try
            {
                var results = await _repository.GetAllTransitPrintersAsync();
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

        //Get a TransitPrinter
        [HttpGet]
        [Route("GetTransitPrinter/{TransitPrinterId}")]
        public async Task<IActionResult> GetTransitPrinter(int TransitPrinterId)
        {
            try
            {
                var result = await _repository.GetTransitPrinterAsync(TransitPrinterId);
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

        // Search TransitPrinter by Name
        [HttpGet]
        [Route("SearchTransitPrinter/{searchString}")]
        public async Task<IActionResult> SearchTransitPrinter(string searchString)
        {
            try
            {
                List<TransitPrinter> results = await _repository.SearchTransitPrinterAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update TransitPrinter
        [HttpPut]
        [Route("UpdateTransitPrinter/{TransitPrinterId}")]
        public async Task<IActionResult> UpdateTransitPrinter(int TransitPrinterId, TransitPrinterViewModal TransitPrinter)
        {
            try
            {
                var result = await _repository.UpdateTransitPrinterAsync(TransitPrinterId, TransitPrinter);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + TransitPrinter.technicianId);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update Transit Printer. Transit Printer not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete TransitPrinter
        [HttpDelete]
        [Route("DeleteTransitPrinter/{TransitPrinterId}")]
        public async Task<IActionResult> DeleteTransitPrinter(int TransitPrinterId)
        {

            try
            {
                var result = await _repository.DeleteTransitPrinterAsync(TransitPrinterId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted TransitPrinter");
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
