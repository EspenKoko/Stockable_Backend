using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockSupplierOrderController : ControllerBase
    {
        private readonly IStockSupplierOrderRepository _repository;

        public StockSupplierOrderController(IStockSupplierOrderRepository repository)
        {
            _repository = repository;
        }

        //Create stockSupplierOrder
        [HttpPost]
        [Route("AddStockSupplierOrder")]
        public async Task<IActionResult> AddStockSupplierOrder(StockSupplierOrderViewModal stockSupplierOrder)
        {
            try
            {
                var result = await _repository.AddStockSupplierOrderAsync(stockSupplierOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added Order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add stock supplier Order the Supplier or the stock could not be found");
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


        //Get all stockSupplierOrderes
        [HttpGet]
        [Route("GetAllStockSupplierOrders")]
        public async Task<IActionResult> GetAllStockSupplierOrderes()
        {
            try
            {
                StockSupplierOrder[] results = await _repository.GetAllStockSupplierOrderAsync();
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

        //Get a stockSupplierOrder
        [HttpGet]
        [Route("GetStockSupplierOrder/{supplierOrderId}")]
        public async Task<IActionResult> GetStockSupplierOrder(int supplierOrderId)
        {
            try
            {
                StockSupplierOrder result = await _repository.GetStockSupplierOrderAsync(supplierOrderId);

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

        // Search stockSupplierOrder by Name
        [HttpGet]
        [Route("SearchStockSupplierOrder/{searchString}")]
        public async Task<IActionResult> SearchStockSupplierOrder(string searchString)
        {
            try
            {
                List<StockSupplierOrder> results = await _repository.SearchStockSupplierOrderAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update stockSupplierOrder
        [HttpPut]
        [Route("UpdateStockSupplierOrder/{supplierOrderId}")]
        public async Task<IActionResult> UpdateStockSupplierOrder(int supplierOrderId, StockSupplierOrderViewModal stockSupplierOrder)
        {
            try
            {
                var result = await _repository.UpdateStockSupplierOrderAsync(supplierOrderId, stockSupplierOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated Order");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update stockSupplierOrder. StockSupplierOrder not be found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update stockSupplierOrder. The  Supplier or the employee or the status could not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete stockSupplierOrder
        [HttpDelete]
        [Route("DeleteStockSupplierOrder/{supplierOrderId}")]
        public async Task<IActionResult> DeleteStockSupplierOrder(int supplierOrderId)
        {
            try
            {
                var result = await _repository.DeleteStockSupplierOrderAsync(supplierOrderId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted stock Supplier Order");
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

        // Delete stockSupplierOrder
        [HttpDelete]
        [Route("DeleteStockSupplierOrderItem")]
        public async Task<IActionResult> DeleteStockSupplierOrderItem(int supplierOrderId, int stockId)
        {
            try
            {
                var result = await _repository.DeleteStockSupplierOrderItemAsync(supplierOrderId, stockId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted stock Supplier Order Item");
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
