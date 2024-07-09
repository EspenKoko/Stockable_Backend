using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditTrailController : ControllerBase
    {
        private readonly IAuditTrailRepository _repository;

        public AuditTrailController(IAuditTrailRepository repository)
        {
            _repository = repository;
        }

        //Create AuditTrail
        [HttpPost]
        [Route("AddAuditTrail")]
        public async Task<IActionResult> AddAuditTrail(AuditTrailViewModal AuditTrail)
        {
            try
            {
                var result = await _repository.AddAuditTrailAsync(AuditTrail);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added AuditTrail");
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

        //Get all AuditTrail
        [HttpGet]
        [Route("GetAllAuditTrail")]
        public async Task<IActionResult> GetAllAuditTrail()
        {
            try
            {
                var results = await _repository.GetAllAuditTrailsAsync();
                if (results.Length == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Audit trails not found");
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

        //Get a AuditTrail
        [HttpGet]
        [Route("GetAuditTrail/{AuditTrailId}")]
        public async Task<IActionResult> GetAuditTrail(int AuditTrailId)
        {
            try
            {
                var result = await _repository.GetAuditTrailAsync(AuditTrailId);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Audit trail not found");
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

        //Update AuditTrail
        [HttpPut]
        [Route("UpdateAuditTrail/{AuditTrailId}")]
        public async Task<IActionResult> UpdateAuditTrail(int AuditTrailId, AuditTrailViewModal AuditTrail)
        {
            try
            {
                var result = await _repository.UpdateAuditTrailAsync(AuditTrailId, AuditTrail);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated AuditTrail");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update AuditTrail. AuditTrail not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete AuditTrail
        [HttpDelete]
        [Route("DeleteAuditTrail/{AuditTrailId}")]
        public async Task<IActionResult> DeleteAuditTrail(int AuditTrailId)
        {

            try
            {
                var result = await _repository.DeleteAuditTrailAsync(AuditTrailId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted AuditTrail");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Audit trail not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }




        //[HttpGet]
        //[Route("GetAuditTrailsByDateAndUser")]
        //public IActionResult GetAuditTrailsByDateAndUser(DateTime startDate, DateTime endDate, string userId)
        //{
        //    try
        //    {
        //        // Set up parameters for the stored procedure
        //        var startDateParameter = new SqlParameter("@StartDate", startDate);
        //        var endDateParameter = new SqlParameter("@EndDate", endDate);
        //        var userIdParameter = new SqlParameter("@UserId", userId);

        //        // Execute the stored procedure
        //        var results = _repository.GetAuditTrailsByDateAndUser(startDateParameter, endDateParameter, userIdParameter);

        //        if (results == null || !results.Any())
        //        {
        //            return StatusCode(StatusCodes.Status404NotFound, "No results found");
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status200OK, results);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
        //    }
        //}

    }
}
