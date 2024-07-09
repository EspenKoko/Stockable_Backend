using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderStatusController : ControllerBase
    {
        private readonly ISupplierOrderStatusRepository _repository;

        public SupplierOrderStatusController(ISupplierOrderStatusRepository repository)
        {
            _repository = repository;
        }


        //Create SupplierOrderStatus
        [HttpPost]
        [Route("AddSupplierOrderStatus")]
        public async Task<IActionResult> AddSupplierOrderStatus(SupplierOrderStatusViewModal supplierOrderStatus)
        {
            try
            {
                var result = await _repository.AddSupplierOrderStatusAsync(supplierOrderStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + supplierOrderStatus.supplierOrderStatusName);
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


        //Get all SupplierOrderStatuss
        [HttpGet]
        [Route("GetAllSupplierOrderStatuses")]
        public async Task<IActionResult> GetAllSupplierOrderStatuss()
        {
            try
            {
                SupplierOrderStatus[] results = await _repository.GetAllSupplierOrderStatusAsync();
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

        //Get a SupplierOrderStatus
        [HttpGet]
        [Route("GetSupplierOrderStatus/{supplierOrderStatusId}")]
        public async Task<IActionResult> GetSupplierOrderStatus(int supplierOrderStatusId)
        {
            try
            {
                SupplierOrderStatus result = await _repository.GetSupplierOrderStatusAsync(supplierOrderStatusId);

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

        // Search SupplierOrderStatus by Name
        [HttpGet]
        [Route("SearchSupplierOrderStatus/{searchString}")]
        public async Task<IActionResult> SearchSupplierOrderStatus(string searchString)
        {
            try
            {
                List<SupplierOrderStatus> results = await _repository.SearchSupplierOrderStatusAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update SupplierOrderStatus
        [HttpPut]
        [Route("UpdateSupplierOrderStatus/{supplierOrderStatusId}")]
        public async Task<IActionResult> UpdateSupplierOrderStatus(int supplierOrderStatusId, SupplierOrderStatusViewModal supplierOrderStatus)
        {
            try
            {
                var result = await _repository.UpdateSupplierOrderStatusAsync(supplierOrderStatusId, supplierOrderStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + supplierOrderStatus.supplierOrderStatusName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update supplierOrderStatus. SupplierOrderStatus not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete SupplierOrderStatus
        [HttpDelete]
        [Route("DeleteSupplierOrderStatus/{supplierOrderStatusId}")]
        public async Task<IActionResult> DeleteSupplierOrderStatus(int supplierOrderStatusId)
        {

            try
            {
                var result = await _repository.DeleteSupplierOrderStatusAsync(supplierOrderStatusId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted supplierOrderStatus");
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
