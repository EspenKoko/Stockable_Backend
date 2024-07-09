using Stockable_Backend.Repository.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairOrdersController : ControllerBase
    {
        private readonly IRepairOrdersRepository _repository;

        public RepairOrdersController(IRepairOrdersRepository repository)
        {
            _repository = repository;
        }

        //Create RepairOrder
        [HttpPost]
        [Route("AddRepairOrder")]
        public async Task<IActionResult> AddRepairOrder(RepairOrdersViewModal RepairOrder)
        {
            try
            {
                var result = await _repository.AddRepairOrderAsync(RepairOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added RepairOrder");
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

        //Get all RepairOrders
        [HttpGet]
        [Route("GetAllRepairOrders")]
        public async Task<IActionResult> GetAllRepairOrders()
        {
            try
            {
                var results = await _repository.GetAllRepairOrdersAsync();
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

        //Get a RepairOrder
        [HttpGet]
        [Route("GetRepairOrder/{RepairOrderId}")]
        public async Task<IActionResult> GetRepairOrder(int RepairOrderId)
        {
            try
            {
                var result = await _repository.GetRepairOrderAsync(RepairOrderId);
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

        //Update RepairOrder
        [HttpPut]
        [Route("UpdateRepairOrder/{RepairOrderId}")]
        public async Task<IActionResult> UpdateRepairOrder(int RepairOrderId, RepairOrdersViewModal RepairOrder)
        {
            try
            {
                var result = await _repository.UpdateRepairOrderAsync(RepairOrderId, RepairOrder);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated RepairOrder");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update RepairOrder. RepairOrder not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete RepairOrder
        [HttpDelete]
        [Route("DeleteRepairOrder/{RepairOrderId}")]
        public async Task<IActionResult> DeleteRepairOrder(int RepairOrderId)
        {

            try
            {
                var result = await _repository.DeleteRepairOrderAsync(RepairOrderId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted RepairOrder");
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
