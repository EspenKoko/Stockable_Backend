using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientInvoiceController : ControllerBase
    {
        private readonly IClientInvoiceRepository _repository;

        public ClientInvoiceController(IClientInvoiceRepository repository)
        {
            _repository = repository;
        }

        //Create clientInvoice
        [HttpPost]
        [Route("AddClientInvoice")]
        public async Task<IActionResult> AddClientInvoice(ClientInvoiceViewModal clientInvoice)
        {
            try
            {
                var result = await _repository.AddClientInvoiceAsync(clientInvoice);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + clientInvoice.clientInvoiceNumber);
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


        //Get all clientInvoices
        [HttpGet]
        [Route("GetAllClientInvoices")]
        public async Task<IActionResult> GetAllClientInvoicees()
        {
            try
            {
                ClientInvoice[] results = await _repository.GetAllClientInvoiceAsync();
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

        //Get a clientInvoice
        [HttpGet]
        [Route("GetClientInvoice/{clientInvoiceId}")]
        public async Task<IActionResult> GetClientInvoice(int clientInvoiceId)
        {
            try
            {
                ClientInvoice result = await _repository.GetClientInvoiceAsync(clientInvoiceId);

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

        // Search clientInvoice by Name
        [HttpGet]
        [Route("SearchClientInvoice/{searchString}")]
        public async Task<IActionResult> SearchClientInvoice(string searchString)
        {
            try
            {
                List<ClientInvoice> results = await _repository.SearchClientInvoiceAsync(searchString);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update clientInvoice
        [HttpPut]
        [Route("UpdateClientInvoice/{clientInvoiceId}")]
        public async Task<IActionResult> UpdateClientInvoice(int clientInvoiceId, ClientInvoiceViewModal clientInvoice)
        {
            try
            {
                var result = await _repository.UpdateClientInvoiceAsync(clientInvoiceId, clientInvoice);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + clientInvoice.clientInvoiceNumber);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update clientInvoice. ClientInvoice not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete clientInvoice
        [HttpDelete]
        [Route("DeleteClientInvoice/{clientInvoiceId}")]
        public async Task<IActionResult> DeleteClientInvoice(int clientInvoiceId)
        {
            try
            {
                var result = await _repository.DeleteClientInvoiceAsync(clientInvoiceId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted clientInvoice");
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
