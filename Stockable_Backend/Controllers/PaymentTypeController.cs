using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeRepository _repository;

        public PaymentTypeController(IPaymentTypeRepository repository)
        {
            _repository = repository;
        }

        //Create paymentType
        [HttpPost]
        [Route("AddPaymentType")]
        public async Task<IActionResult> AddPaymentType(PaymentTypeViewModal paymentType)
        {
            try
            {
                var result = await _repository.AddPaymentTypeAsync(paymentType);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + paymentType.paymentTypeName);
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


        //Get all paymentTypees
        [HttpGet]
        [Route("GetAllPaymentTypes")]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            try
            {
                PaymentType[] results = await _repository.GetAllPaymentTypeAsync();
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

        //Get a paymentType
        [HttpGet]
        [Route("GetPaymentType/{paymentTypeId}")]
        public async Task<IActionResult> GetPaymentType(int paymentTypeId)
        {
            try
            {
                PaymentType result = await _repository.GetPaymentTypeAsync(paymentTypeId);

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

        // Search paymentType by Name
        [HttpGet]
        [Route("SearchPaymentType/{searchString}")]
        public async Task<IActionResult> SearchPaymentType(string searchString)
        {
            try
            {
                List<PaymentType> results = await _repository.SearchPaymentTypeAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update paymentType
        [HttpPut]
        [Route("UpdatePaymentType/{paymentTypeId}")]
        public async Task<IActionResult> UpdatePaymentType(int paymentTypeId, PaymentTypeViewModal paymentType)
        {
            try
            {
                var result = await _repository.UpdatePaymentTypeAsync(paymentTypeId, paymentType);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + paymentType.paymentTypeName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update paymentType. PaymentType not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete paymentType
        [HttpDelete]
        [Route("DeletePaymentType/{paymentTypeId}")]
        public async Task<IActionResult> DeletePaymentType(int paymentTypeId)
        {
            try
            {
                var result = await _repository.DeletePaymentTypeAsync(paymentTypeId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted paymentType");
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
