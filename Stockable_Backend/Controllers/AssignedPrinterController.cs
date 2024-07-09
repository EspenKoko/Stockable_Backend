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
    public class AssignedPrinterController : ControllerBase
    {
        private readonly IAssignedPrinterRepository _repository;

        public AssignedPrinterController(IAssignedPrinterRepository repository)
        {
            _repository = repository;
        }


        //Create Printer
        [HttpPost]
        [Route("AddPrinter")]
        public async Task<IActionResult> AddPrinter(AssignedPrinterViewModal printer)
        {
            try
            {
                var result = await _repository.AddPrinterAsync(printer);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + printer.serialNumber);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add Printer. The client, printerModel, printerStatus, branch, city could not be found");
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


        //Get all Printer
        [HttpGet]
        [Route("GetAllPrinters")]
        public async Task<IActionResult> GetAllPrinters()
        {
            try
            {
                var results = await _repository.GetAllPrinterAsync();
                if (results.Length == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No printers found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, results);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Get a Printer
        [HttpGet]
        [Route("GetPrinter/{printerId}")]
        public async Task<IActionResult> GetPrinter(int printerId)
        {
            try
            {
                var result = await _repository.GetPrinterAsync(printerId);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Printer not found");
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

        // Search Printer by Name
        [HttpGet]
        [Route("SearchPrinter/{searchString}")]
        public async Task<IActionResult> SearchPrinter(string searchString)
        {
            try
            {
                var results = await _repository.SearchPrinterAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update Printer
        [HttpPut]
        [Route("UpdatePrinter/{printerId}")]
        public async Task<IActionResult> UpdatePrinter(int printerId, AssignedPrinterViewModal printer)
        {
            try
            {
                var result = await _repository.UpdatePrinterAsync(printerId, printer);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + printer.serialNumber);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update printer. Printer not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update Printer. The client, printerModel, printerStatus, branch, city could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete Printer
        [HttpDelete]
        [Route("DeletePrinter/{printerId}")]
        public async Task<IActionResult> DeletePrinter(int printerId)
        {

            try
            {
                var result = await _repository.DeletePrinterAsync(printerId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted printer");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Printer not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }
    }
}
