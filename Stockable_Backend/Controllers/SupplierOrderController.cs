using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderController : ControllerBase
    {
        private readonly ISupplierOrderRepository _repository;

        public SupplierOrderController(ISupplierOrderRepository repository)
        {
            _repository = repository;
        }

        //Create supplierOrder
        [HttpPost]
        [Route("AddSupplierOrder")]
        public async Task<IActionResult> AddSupplierOrder(SupplierOrderViewModal supplierOrder)
        {
            try
            {
                var result = await _repository.AddSupplierOrderAsync(supplierOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added Order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add supplier Order the Supplier or the employee or the status could not be found");
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


        //Get all supplierOrderes
        [HttpGet]
        [Route("GetAllSupplierOrders")]
        public async Task<IActionResult> GetAllSupplierOrders()
        {
            try
            {
                SupplierOrder[] results = await _repository.GetAllSupplierOrdersAsync();
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

        //Get a supplierOrder
        [HttpGet]
        [Route("GetSupplierOrder/{supplierOrderId}")]
        public async Task<IActionResult> GetSupplierOrder(int supplierOrderId)
        {
            try
            {
                SupplierOrder result = await _repository.GetSupplierOrderAsync(supplierOrderId);

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

        // Search supplierOrder by Name
        [HttpGet]
        [Route("SearchSupplierOrder/{searchString}")]
        public async Task<IActionResult> SearchSupplierOrder(string searchString)
        {
            try
            {
                List<SupplierOrder> results = await _repository.SearchSupplierOrderAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update supplierOrder
        [HttpPut]
        [Route("UpdateSupplierOrder/{brarchId}")]
        public async Task<IActionResult> UpdateSupplierOrder(int brarchId, SupplierOrderViewModal supplierOrder)
        {
            try
            {
                var result = await _repository.UpdateSupplierOrderAsync(brarchId, supplierOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated Order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update supplierOrder. SupplierOrder not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update supplierOrder. The  Supplier or the employee or the status could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete supplierOrder
        [HttpDelete]
        [Route("DeleteSupplierOrder/{supplierOrderId}")]
        public async Task<IActionResult> DeleteSupplierOrder(int supplierOrderId)
        {
            try
            {
                var result = await _repository.DeleteSupplierOrderAsync(supplierOrderId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted supplierOrder");
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
