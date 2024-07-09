using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairDiagnosticController : ControllerBase
    {
        private readonly IRepairDiagnosticRepository _repository;

        public RepairDiagnosticController(IRepairDiagnosticRepository repository)
        {
            _repository = repository;
        }

        //Create repairDiagnostics
        [HttpPost]
        [Route("AddRepairDiagnostics")]
        public async Task<IActionResult> AddRepairDiagnostics(RepairDiagnosticViewModal repairDiagnostics)
        {
            try
            {
                var result = await _repository.AddRepairDiagnosticsAsync(repairDiagnostics);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add Purchase Order Diagnostic: Diagnostics or Purchase Order was not found");
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

        //Get all repairDiagnosticses
        [HttpGet]
        [Route("GetAllRepairDiagnostics")]
        public async Task<IActionResult> GetAllRepairDiagnosticses()
        {
            try
            {
                RepairDiagnostic[] results = await _repository.GetAllRepairDiagnosticsAsync();
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

        //Get a repairDiagnostics
        [HttpGet]
        [Route("GetRepairDiagnostics/{repairId}")]
        public async Task<IActionResult> GetRepairDiagnostics(int repairId)
        {
            try
            {
                RepairDiagnostic result = await _repository.GetRepairDiagnosticsAsync(repairId);

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

        // Search repairDiagnostics by Name
        [HttpGet]
        [Route("SearchRepairDiagnostics/{searchString}")]
        public async Task<IActionResult> SearchRepairDiagnostics(string searchString)
        {
            try
            {
                List<RepairDiagnostic> results = await _repository.SearchRepairDiagnosticsAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update repairDiagnostics
        [HttpPut]
        [Route("UpdateRepairDiagnostics/{printerId}")]
        public async Task<IActionResult> UpdateRepairDiagnostics(int printerId, RepairDiagnosticViewModal repairDiagnostics)
        {
            try
            {
                var result = await _repository.UpdateRepairDiagnosticsAsync(printerId, repairDiagnostics);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update Purchase Order Diagnostic. Purchase Order Diagnostic not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update Purchase Order Diagnostic. The Diagnostics or Purchase Order could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete repairDiagnostics
        [HttpDelete]
        [Route("DeleteRepairDiagnostics/{repairId}")]
        public async Task<IActionResult> DeleteRepairDiagnostics(int repairId)
        {
            try
            {
                var result = await _repository.DeleteRepairDiagnosticsAsync(repairId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Purchase Order Diagnostic");
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
