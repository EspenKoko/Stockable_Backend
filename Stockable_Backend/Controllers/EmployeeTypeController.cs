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
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IEmployeeTypeRepository _repository;

        public EmployeeTypeController(IEmployeeTypeRepository repository)
        {
            _repository = repository;
        }


        //Create employeetypes
        [HttpPost]
        [Route("AddEmployeeType")]
        public async Task<IActionResult> AddEmployeeType(EmployeeTypeViewModal employeeType)
        {
            try
            {
                var result = await _repository.AddEmployeeTypeAsync(employeeType);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + employeeType.employeeTypeName);
                }
                else if(result == 400)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Employee type name invalid. cannot contain numbers, spaces or special charactors");
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


        //Get all EmployeeTypes
        [HttpGet]
        [Route("GetAllEmployeeTypes")]
        public async Task<IActionResult> GetAllEmployeeType()
        {
            try
            {
                EmployeeType[] results = await _repository.GetAllEmployeeTypesAsync();
                if (results.Length == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Employee type not found");
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

        //Get a Employeetype
        [HttpGet]
        [Route("GetEmployeeType/{employeeTypeId}")]
        public async Task<IActionResult> GetCourses(int employeeTypeId)
        {
            try
            {
                EmployeeType result = await _repository.GetEmployeeTypeAsync(employeeTypeId);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Employee type not found");
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

        // Search Employeetype by Name
        [HttpGet]
        [Route("SearchEmployeetype/{searchString}")]
        public async Task<IActionResult> SearchEmployeetype(string searchString)
        {
            try
            {
                List<EmployeeType> results = await _repository.SearchEmployeeTypeAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update Employeetype
        [HttpPut]
        [Route("UpdateEmployeeType/{employeeTypeId}")]
        public async Task<IActionResult> UpdateEmployeeType(int employeeTypeId, EmployeeTypeViewModal employeeType)
        {
            try
            {
                var result = await _repository.UpdateEmployeeTypeAsync(employeeTypeId, employeeType);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + employeeType.employeeTypeName);
                }
                else if(result == 400)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Please enter a valid Employeetype name with letters and spaces only");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update employeeType. employeeType not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        // Delete employee
        [HttpDelete]
        [Route("DeleteEmployeeType/{employeeTypeId}")]
        public async Task<IActionResult> DeleteEmployeeType(int employeeTypeId)
        {

            try
            {
                var result = await _repository.DeleteEmployeeTypeAsync(employeeTypeId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted employeeType");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Employee type not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        [HttpGet]
        [Route("GetAllEmployeeTypesFromStoredProcedure")]
        public async Task<IActionResult> GetAllEmployeeTypesFromStoredProcedure()
        {
            try
            {
                var results = await _repository.GetAllEmployeeTypesFromStoredProcedureAsync();
                if (results == null || !results.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Employee types not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, results);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred on the server, please try again later");
            }
        }


    }
}
