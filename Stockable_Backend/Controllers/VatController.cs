using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatController : ControllerBase
    {
        private readonly IVatRepository _repository;

        public VatController(IVatRepository repository)
        {
            _repository = repository;
        }


        //Create Vat
        [HttpPost]
        [Route("AddVat")]
        public async Task<IActionResult> AddVat(VatViewModal vat)
        {
            try
            {
                var result = await _repository.AddVatAsync(vat);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + vat.vatPercent);
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


        //Get all vats
        [HttpGet]
        [Route("GetAllVats")]
        public async Task<IActionResult> GetAllVats()
        {
            try
            {
                var results = await _repository.GetAllVatsAsync();
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

        //Get a Vat
        [HttpGet]
        [Route("GetVat/{vatId}")]
        public async Task<IActionResult> GetVat(int vatId)
        {
            try
            {
                var result = await _repository.GetVatAsync(vatId);
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

        //Update vat
        [HttpPut]
        [Route("UpdateVat/{vatId}")]
        public async Task<IActionResult> UpdateVat(int vatId, VatViewModal vat)
        {
            try
            {
                var result = await _repository.UpdateVatAsync(vatId, vat);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + vat.vatPercent);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update vat. Vat not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete vat
        [HttpDelete]
        [Route("DeleteVat/{vatId}")]
        public async Task<IActionResult> DeleteVat(int vatId)
        {

            try
            {
                var result = await _repository.DeleteVatAsync(vatId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted vat entry");
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
