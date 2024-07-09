//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Stockable_Backend.Model;
//using Stockable_Backend.Repository;
//using Stockable_Backend.Repository.IRepositories;
//using Stockable_Backend.ViewModel;

//namespace Stockable_Backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PrinterModelController : ControllerBase
//    {
//        private readonly IPrinterModelRepository _repository;

//        public PrinterModelController(IPrinterModelRepository repository)
//        {
//            _repository = repository;
//        }


//        //Create PrinterModel
//        [HttpPost]
//        [Route("AddPrinterModel")]
//        public async Task<IActionResult> AddPrinterModel(PrinterModelViewModal printerModel)
//        {
//            try
//            {
//                var result = await _repository.AddPrinterModelAsync(printerModel);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully Added " + printerModel.printerModelName);
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status500InternalServerError);
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }


//        //Get all PrinterModels
//        [HttpGet]
//        [Route("GetAllPrinterModels")]
//        public async Task<IActionResult> GetAllPrinterModels()
//        {
//            try
//            {
//                var results = await _repository.GetAllPrinterModelsAsync();
//                if (results.Length == 0)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status200OK, results);
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Get a PrinterModel
//        [HttpGet]
//        [Route("GetPrinterModel/{printerModelId}")]
//        public async Task<IActionResult> GetPrinterModel(int printerModelId)
//        {
//            try
//            {
//                var result = await _repository.GetPrinterModelAsync(printerModelId);

//                if (result == null)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status200OK, result);
//                }
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        // Search PrinterModel by Name
//        [HttpGet]
//        [Route("SearchPrinterModel/{searchString}")]
//        public async Task<IActionResult> SearchPrinterModel(string searchString)
//        {
//            try
//            {
//                var results = await _repository.SearchPrinterModelAsync(searchString);
//                return Ok(results);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Update PrinterModel
//        [HttpPut]
//        [Route("UpdatePrinterModel/{printerModelId}")]
//        public async Task<IActionResult> UpdatePrinterModel(int printerModelId, PrinterModelViewModal printerModel)
//        {
//            try
//            {
//                var result = await _repository.UpdatePrinterModelAsync(printerModelId, printerModel);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully updated " + printerModel.printerModelName);
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update printer model. Printer model not be found");
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }


//        // Delete PrinterModel
//        [HttpDelete]
//        [Route("DeletePrinterModel/{printerModelId}")]
//        public async Task<IActionResult> DeletePrinterModel(int printerModelId)
//        {

//            try
//            {
//                var result = await _repository.DeletePrinterModelAsync(printerModelId);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted printer model");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }
//    }
//}
