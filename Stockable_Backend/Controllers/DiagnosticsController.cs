using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticsController : ControllerBase
    {
        private readonly IDiagnosticsRepository _repository;

        public DiagnosticsController(IDiagnosticsRepository repository)
        {
            _repository = repository;
        }

        //Create Diagnostics
        [HttpPost]
        [Route("AddDiagnostics")]
        public async Task<IActionResult> AddDiagnostics(DiagnosticsViewModal diagnostics)
        {
            try
            {
                var result = await _repository.AddDiagnosticsAsync(diagnostics);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added diagnostics");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add diagnostic the repair could not be found");
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


        //Get all Diagnostics
        [HttpGet]
        [Route("GetAllDiagnostics")]
        public async Task<IActionResult> GetAllDiagnosticss()
        {
            try
            {
                Diagnostics[] results = await _repository.GetAllDiagnosticsAsync();
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

        //Get a Diagnostics
        [HttpGet]
        [Route("GetDiagnostics/{diagnosticsId}")]
        public async Task<IActionResult> GetDiagnostics(int diagnosticsId)
        {
            try
            {
                Diagnostics result = await _repository.GetDiagnosticsAsync(diagnosticsId);

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

        // Search Diagnostics by Name
        [HttpGet]
        [Route("SearchDiagnostics/{searchString}")]
        public async Task<IActionResult> SearchDiagnostics(string searchString)
        {
            try
            {
                List<Diagnostics> results = await _repository.SearchDiagnosticsAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update Diagnostics
        [HttpPut]
        [Route("UpdateDiagnostics/{diagnosticsId}")]
        public async Task<IActionResult> UpdateDiagnostics(int diagnosticsId, DiagnosticsViewModal diagnostics)
        {
            try
            {
                var result = await _repository.UpdateDiagnosticsAsync(diagnosticsId, diagnostics);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated diagnostics");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update diagnotics. Diagnotics not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update diagnotics. The repair could not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete Diagnostics
        [HttpDelete]
        [Route("DeleteDiagnostics/{diagnosticsId}")]
        public async Task<IActionResult> DeleteDiagnostics(int diagnosticsId)
        {

            try
            {
                var result = await _repository.DeleteDiagnosticsAsync(diagnosticsId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Diagnotics");
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
