using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedTechnicianController : ControllerBase
    {
        private readonly IAssignedTechnicianRepository _repository;

        public AssignedTechnicianController(IAssignedTechnicianRepository repository)
        {
            _repository = repository;
        }

        //Create AssignedTechnician
        [HttpPost]
        [Route("AddAssignedTechnician")]
        public async Task<IActionResult> AddAssignedTechnician(AssignedTechnicianViewModal assignedTechnician)
        {
            try
            {
                var result = await _repository.AddAssignedTechnicianAsync(assignedTechnician);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully assigned technician");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to assign technician. Employee not be found");
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


        //Get all assignedTechnicianes
        [HttpGet]
        [Route("GetAllAssignedTechnicians")]
        public async Task<IActionResult> GetAllAssignedTechnicians()
        {
            try
            {
                AssignedTechnician[] results = await _repository.GetAllAssignedTechnicianAsync();
                if (results.Length == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Assigned Technicains not found");
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

        //Get a assignedTechnician
        [HttpGet]
        [Route("GetAssignedTechnician/{errorLogId}")]
        public async Task<IActionResult> GetAssignedTechnician(int errorLogId)
        {
            try
            {
                List<AssignedTechnician> result = await _repository.GetAssignedTechnicianAsync(errorLogId);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Assigned Technicain not found");
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

        // Search assignedTechnician by serlie Number
        [HttpGet]
        [Route("SearchAssignedTechnician/{searchString}")]
        public async Task<IActionResult> SearchAssignedTechnician(string searchString)
        {
            try
            {
                List<AssignedTechnician> results = await _repository.SearchAssignedTechnicianAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Update assignedTechnician
        [HttpPut]
        [Route("UpdateAssignedTechnician/{errorLogId}")]
        public async Task<IActionResult> UpdateAssignedTechnician(int errorLogId, AssignedTechnicianViewModal assignedTechnician)
        {
            try
            {
                var result = await _repository.UpdateAssignedTechnicianAsync(errorLogId, assignedTechnician);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated assigned technician");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update assigned technician. Assigned Technician not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update assigned technician. The emplyee not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete assignedTechnician
        [HttpDelete]
        [Route("DeleteAssignedTechnician/{errorLogId}")]
        public async Task<IActionResult> DeleteAssignedTechnician(int errorLogId)
        {
            try
            {
                var result = await _repository.DeleteAssignedTechnicianAsync(errorLogId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted assigned technician");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound,"Assigned Technician not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }
    }
}
