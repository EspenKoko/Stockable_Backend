using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;
using System.Collections.Generic;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrinterStatusController : ControllerBase
    {
        private readonly IPrinterStatusRepository _repository;

        public PrinterStatusController(IPrinterStatusRepository repository)
        {
            _repository = repository;
        }


        //Create PrinterStatus
        [HttpPost]
        [Route("AddPrinterStatus")]
        public async Task<IActionResult> AddPrinterStatus(PrinterStatusViewModal printerStatus)
        {
            try
            {
                var result = await _repository.AddPrinterStatusAsync(printerStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + printerStatus.printerStatusName);
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

        //Get all PrinterStatuses
        [HttpGet]
        [Route("GetAllPrinterStatus")]
        public async Task<IActionResult> GetAllPPrinterStatus()
        {
            try
            {
                var results = await _repository.GetAllPrinterStatusAsync();
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

        //Get a PrinterStatus
        [HttpGet]
        [Route("GetPrinterStatus/{printerStatusId}")]
        public async Task<IActionResult> GetPrinterStatus(int printerStatusId)
        {
            try
            {
                var result = await _repository.GetPrinterStatusAsync(printerStatusId);
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

        // Search PrinterStatus by Name
        [HttpGet]
        [Route("SearchPrinterStatus/{searchString}")]
        public async Task<IActionResult> SearchPrinterStatus(string searchString)
        {
            try
            {
                List<PrinterStatus> results = await _repository.SearchPrinterStatusAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update PrinterStatus
        [HttpPut]
        [Route("UpdatePrinterStatus/{printerStatusId}")]
        public async Task<IActionResult> UpdatePrinterStatus(int printerStatusId, PrinterStatusViewModal printerStatus)
        {
            try
            {
                var result = await _repository.UpdatePrinterStatusAsync(printerStatusId, printerStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + printerStatus.printerStatusName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update printer status. Status not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete PrinterStatus
        [HttpDelete]
        [Route("DeletePrinterStatus/{printerStatusId}")]
        public async Task<IActionResult> DeletePrinterStatus(int printerStatusId)
        {

            try
            {
                var result = await _repository.DeletePrinterStatusAsync(printerStatusId);
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
