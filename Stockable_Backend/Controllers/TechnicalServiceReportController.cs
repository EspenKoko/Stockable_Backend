using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalServiceReportController : ControllerBase
    {
        private readonly ITechnicalServiceReportRepository _repository;

        public TechnicalServiceReportController(ITechnicalServiceReportRepository repository)
        {
            _repository = repository;
        }

        //Create technicalServiceReport
        [HttpPost]
        [Route("AddTechnicalServiceReport")]
        public async Task<IActionResult> AddTechnicalServiceReport(TechnicalServiceReportViewModal technicalServiceReport)
        {
            try
            {
                var result = await _repository.AddTechnicalServiceReportAsync(technicalServiceReport);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added Technical Service Report");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add technicalServiceReport the Repair Invoice could not be found");
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


        //Get all technicalServiceReportes
        [HttpGet]
        [Route("GetAllTechnicalServiceReports")]
        public async Task<IActionResult> GetAllTechnicalServiceReportes()
        {
            try
            {
                TechnicalServiceReport[] results = await _repository.GetAllTechnicalServiceReportAsync();
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

        //Get a technicalServiceReport
        [HttpGet]
        [Route("GetTechnicalServiceReport/{technicalServiceReportId}")]
        public async Task<IActionResult> GetTechnicalServiceReport(int technicalServiceReportId)
        {
            try
            {
                TechnicalServiceReport result = await _repository.GetTechnicalServiceReportAsync(technicalServiceReportId);

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

        // Search technicalServiceReport by Name
        [HttpGet]
        [Route("SearchTechnicalServiceReport/{searchString}")]
        public async Task<IActionResult> SearchTechnicalServiceReport(string searchString)
        {
            try
            {
                List<TechnicalServiceReport> results = await _repository.SearchTechnicalServiceReportAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update technicalServiceReport
        [HttpPut]
        [Route("UpdateTechnicalServiceReport/{technicalServiceReportId}")]
        public async Task<IActionResult> UpdateTechnicalServiceReport(int technicalServiceReportId, TechnicalServiceReportViewModal technicalServiceReport)
        {
            try
            {
                var result = await _repository.UpdateTechnicalServiceReportAsync(technicalServiceReportId, technicalServiceReport);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated Technical Service Report");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update Technical Service Report. Technical Service Report not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update technicalServiceReport. The Repair Invoice could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete technicalServiceReport
        [HttpDelete]
        [Route("DeleteTechnicalServiceReport/{technicalServiceReportId}")]
        public async Task<IActionResult> DeleteTechnicalServiceReport(int technicalServiceReportId)
        {
            try
            {
                var result = await _repository.DeleteTechnicalServiceReportAsync(technicalServiceReportId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Technical Service Report");
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
