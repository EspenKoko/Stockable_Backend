using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _repository;

        public BranchController(IBranchRepository repository)
        {
            _repository = repository;
        }

        //Create branch
        [HttpPost]
        [Route("AddBranch")]
        public async Task<IActionResult> AddBranch(BranchViewModal branch)
        {
            try
            {
                var result = await _repository.AddBranchAsync(branch);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + branch.branchName);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add branch the city or the client could not be found");
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


        //Get all branches
        [HttpGet]
        [Route("GetAllBranches")]
        public async Task<IActionResult> GetAllBranches()
        {
            try
            {
                Branch[] results = await _repository.GetAllBranchAsync();
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

        //Get a branch
        [HttpGet]
        [Route("GetBranch/{branchId}")]
        public async Task<IActionResult> GetBranch(int branchId)
        {
            try
            {
                Branch result = await _repository.GetBranchAsync(branchId);

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

        // Search branch by Name
        [HttpGet]
        [Route("SearchBranch/{searchString}")]
        public async Task<IActionResult> SearchBranch(string searchString)
        {
            try
            {
                List<Branch> results = await _repository.SearchBranchAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update branch
        [HttpPut]
        [Route("UpdateBranch/{brarchId}")]
        public async Task<IActionResult> UpdateBranch(int brarchId, BranchViewModal branch)
        {
            try
            {
                var result = await _repository.UpdateBranchAsync(brarchId, branch);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + branch.branchName);
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update branch. Branch not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update branch. The city or the client could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete branch
        [HttpDelete]
        [Route("DeleteBranch/{branchId}")]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            try
            {
                var result = await _repository.DeleteBranchAsync(branchId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted branch");
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
