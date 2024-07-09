using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFHelpDocController : ControllerBase
    {
        private readonly IPDFHelpDocRepository _repository;

        public PDFHelpDocController(IPDFHelpDocRepository repository)
        {
            _repository = repository;
        }

        // Create PDFHelpDoc
        [HttpPost]
        [Route("AddPDFHelpDoc")]
        public async Task<IActionResult> AddPDFHelpDoc([FromForm] PDFHelpDocViewModal model)
        {
            try
            {
                if (model.pdfFile != null && model.pdfFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.pdfFile.CopyToAsync(memoryStream);
                        var pdfContent = memoryStream.ToArray();

                        var pdfHelpDoc = new PDFHelpDoc
                        {
                            userType = model.userType,
                            pdfContent = pdfContent
                        };

                        await _repository.AddPDFHelpDocAsync(pdfHelpDoc);

                        return StatusCode(StatusCodes.Status200OK, "Successfully Added PDFHelpDoc");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No PDF file provided.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        // Get all PDFHelpDoc
        [HttpGet]
        [Route("GetAllPDFHelpDoc")]
        public async Task<IActionResult> GetAllPDFHelpDoc()
        {
            try
            {
                var results = await _repository.GetAllPDFHelpDocsAsync();
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

        // Update PDFHelpDoc
        [HttpPut]
        [Route("UpdatePDFHelpDoc/{PDFHelpDocId}")]
        public async Task<IActionResult> UpdatePDFHelpDoc(int PDFHelpDocId, [FromForm] PDFHelpDocViewModal model)
        {
            try
            {
                if (model.pdfFile != null && model.pdfFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.pdfFile.CopyToAsync(memoryStream);
                        var pdfContent = memoryStream.ToArray();

                        var pdfHelpDoc = new PDFHelpDocViewModal
                        {
                            userType = model.userType,
                            pdfFile = model.pdfFile // Pass the IFormFile
                        };

                        var result = await _repository.UpdatePDFHelpDocAsync(PDFHelpDocId, pdfHelpDoc);

                        if (result == 200)
                        {
                            return StatusCode(StatusCodes.Status200OK, "Successfully updated PDFHelpDoc");
                        }
                        else if (result == 404)
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Failed to update PDFHelpDoc. PDFHelpDoc not found.");
                        }
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No PDF file provided.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update PDFHelpDoc.");
        }


        // Delete PDFHelpDoc
        [HttpDelete]
        [Route("DeletePDFHelpDoc/{PDFHelpDocId}")]
        public async Task<IActionResult> DeletePDFHelpDoc(int PDFHelpDocId)
        {
            try
            {
                var result = await _repository.DeletePDFHelpDocAsync(PDFHelpDocId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted PDFHelpDoc");
                }
                else if (result == 404)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "PDFHelpDoc not found.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete PDFHelpDoc.");
        }
    }
}
