using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable.Model;
using Stockable_Backend.Model;
using Stockable_Backend.Repository;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;
using System.Net.Security;


namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }


        //Create employee
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeViewModal employee)
        {
            try
            {
                var result = await _repository.AddEmployeeAsync(employee);


                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added employee");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add Employee. The empployeetype or user could not be found");
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


        //Get all Employees
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                Employee[] results = await _repository.GetAllEmployeesAsync();
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

        //Get a Employee
        [HttpGet]
        [Route("GetEmployee/{employeeId}")]
        public async Task<IActionResult> GetCourses(int employeeId)
        {
            try
            {
                Employee result = await _repository.GetEmployeeAsync(employeeId);
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

        // Search Employee by Name
        [HttpGet]
        [Route("SearchEmployee/{searchString}")]
        public async Task<IActionResult> SearchEmployee(string searchString)
        {
            try
            {
                List<Employee> results = await _repository.SearchEmployeeAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update Employee
        [HttpPut]
        [Route("UpdateEmployee/{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, EmployeeViewModal employee)
        {
            try
            {
                var result = await _repository.UpdateEmployeeAsync(employeeId, employee);

  
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated employee" );
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update employee. Employee not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update employee. The employeetype or the user could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete employee
        [HttpDelete]
        [Route("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {

            try
            {
                var result = await _repository.DeleteEmployeeAsync(employeeId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted employee");
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
