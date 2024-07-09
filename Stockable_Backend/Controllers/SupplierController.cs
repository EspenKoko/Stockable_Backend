using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _repository;

        public SupplierController(ISupplierRepository repository)
        {
            _repository = repository;
        }


        //Create Supplier
        [HttpPost]
        [Route("AddSupplier")]
        public async Task<IActionResult> AddSupplier(SupplierViewModal supplier)
        {
            try
            {
                var result = await _repository.AddSupplierAsync(supplier);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + supplier.supplierName);
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


        //Get all Suppliers
        [HttpGet]
        [Route("GetAllSupplier")]
        public async Task<IActionResult> GetAllSupplier()
        {
            try
            {
                var results = await _repository.GetAllSuppliersAsync();
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

        //Get a Supplier
        [HttpGet]
        [Route("GetSupplier/{supplierId}")]
        public async Task<IActionResult> GetSupplier(int supplierId)
        {
            try
            {
                var result = await _repository.GetSupplierAsync(supplierId);
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

        // Search Supplier by Name
        [HttpGet]
        [Route("SearchSupplier/{searchString}")]
        public async Task<IActionResult> SearchSupplier(string searchString)
        {
            try
            {
                var results = await _repository.SearchSupplierAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update Supplier
        [HttpPut]
        [Route("UpdateSupplier/{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(int supplierId, SupplierViewModal supplier)
        {
            try
            {
                var result = await _repository.UpdateSupplierAsync(supplierId, supplier);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + supplier.supplierName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update supplier. Supplier not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete Supplier
        [HttpDelete]
        [Route("DeleteSupplier/{supplierId}")]
        public async Task<IActionResult> DeleteSupplier(int supplierId)
        {

            try
            {
                var result = await _repository.DeleteSupplierAsync(supplierId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted supplier");
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
