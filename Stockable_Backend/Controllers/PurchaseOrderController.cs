using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _repository;

        public PurchaseOrderController(IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        //Create purchaseOrder
        [HttpPost]
        [Route("AddPurchaseOrder")]
        public async Task<IActionResult> AddPurchaseOrder(PurchaseOrderViewModal purchaseOrder)
        {
            try
            {
                var result = await _repository.AddPurchaseOrderAsync(purchaseOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added Purchase Order" );
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add purchase Order the repair or the order status could not be found");
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


        //Get all purchaseOrder
        [HttpGet]
        [Route("GetAllPurchaseOrders")]
        public async Task<IActionResult> GetAllPurchaseOrderes()
        {
            try
            {
                PurchaseOrder[] results = await _repository.GetAllPurchaseOrderAsync();
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

        //Get a purchaseOrder
        [HttpGet]
        [Route("GetPurchaseOrder/{invoiceId}")]
        public async Task<IActionResult> GetPurchaseOrder(int invoiceId)
        {
            try
            {
                PurchaseOrder result = await _repository.GetPurchaseOrderAsync(invoiceId);

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

        // Search purchaseOrder by Name
        [HttpGet]
        [Route("SearchPurchaseOrder/{searchString}")]
        public async Task<IActionResult> SearchPurchaseOrder(string searchString)
        {
            try
            {
                List<PurchaseOrder> results = await _repository.SearchPurchaseOrderAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update purchaseOrder
        [HttpPut]
        [Route("UpdatePurchaseOrder/{invoiceId}")]
        public async Task<IActionResult> UpdatePurchaseOrder(int invoiceId, PurchaseOrderViewModal purchaseOrder)
        {
            try
            {
                var result = await _repository.UpdatePurchaseOrderAsync(invoiceId, purchaseOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated purchase Order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update purchase Order. Purchase Order not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update purchaseOrder. The rapair or the order status could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete purchaseOrder
        [HttpDelete]
        [Route("DeletePurchaseOrder/{invoiceId}")]
        public async Task<IActionResult> DeletePurchaseOrder(int invoiceId)
        {
            try
            {
                var result = await _repository.DeletePurchaseOrderAsync(invoiceId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted purchase order");
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
