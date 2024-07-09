using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderStatusController : ControllerBase
    {
        private readonly IPurchaseOrderStatusRepository _repository;

        public PurchaseOrderStatusController(IPurchaseOrderStatusRepository repository)
        {
            _repository = repository;
        }

        //Create PurchaseOrderStatus
        [HttpPost]
        [Route("AddPurchaseOrderStatus")]
        public async Task<IActionResult> AddPurchaseOrderStatus(PurchaseOrderStatusViewModal purchaseOrderStatus)
        {
            try
            {
                var result = await _repository.AddPurchaseOrderStatusAsync(purchaseOrderStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + purchaseOrderStatus.purchaseOrderStatusName);
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


        //Get all PurchaseOrderStatus
        [HttpGet]
        [Route("GetAllPurchaseOrderStatuses")]
        public async Task<IActionResult> GetAllPurchaseOrderStatuses()
        {
            try
            {
                PurchaseOrderStatus[] results = await _repository.GetAllPurchaseOrderStatusAsync();
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

        //Get a PurchaseOrderStatus
        [HttpGet]
        [Route("GetPurchaseOrderStatus/{purchaseOrderStatusId}")]
        public async Task<IActionResult> GetPurchaseOrderStatus(int purchaseOrderStatusId)
        {
            try
            {
                PurchaseOrderStatus result = await _repository.GetPurchaseOrderStatusAsync(purchaseOrderStatusId);

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

        // Search PurchaseOrderStatus by Name
        [HttpGet]
        [Route("SearchPurchaseOrderStatus/{searchString}")]
        public async Task<IActionResult> SearchPurchaseOrderStatus(string searchString)
        {
            try
            {
                List<PurchaseOrderStatus> results = await _repository.SearchPurchaseOrderStatusAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update PurchaseOrderStatus
        [HttpPut]
        [Route("UpdatePurchaseOrderStatus/{purchaseOrderStatusId}")]
        public async Task<IActionResult> UpdatePurchaseOrderStatus(int purchaseOrderStatusId, PurchaseOrderStatusViewModal purchaseOrderStatus)
        {
            try
            {
                var result = await _repository.UpdatePurchaseOrderStatusAsync(purchaseOrderStatusId, purchaseOrderStatus);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + purchaseOrderStatus.purchaseOrderStatusName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update Order Status. Order Status not be found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete PurchaseOrderStatus
        [HttpDelete]
        [Route("DeletePurchaseOrderStatus/{purchaseOrderStatusId}")]
        public async Task<IActionResult> DeletePurchaseOrderStatus(int purchaseOrderStatusId)
        {

            try
            {
                var result = await _repository.DeletePurchaseOrderStatusAsync(purchaseOrderStatusId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Order Status");
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
